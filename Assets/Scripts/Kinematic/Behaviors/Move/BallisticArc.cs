using System;
using UnityEngine;

namespace Behaviors.Move
{
    public class BallisticArc : FollowPath
    {
        public float muzzleVelocity = 40f;
        public Vector3 gravity = Vector3.down * 9.81f;

        protected Vector3 start;
        public Vector3? target;

        public bool longerArc = false;
        public int pathSteps = 20;

        public BallisticArc(BaseKinematic character) : base(character)
        {
        }

        public void UpdateTarget(Vector3? target)
        {
            this.target = target;
            Debug.Log(target);
            start = character.transform.position;
            if (!target.HasValue)
            {
                path = null;
                return;
            }
            CalculateFiring();
        }

        public void CalculateFiring()
        {
            Vector3 delta = target.Value - start;
            
            // calculate the real-valued a,b,c coefficients of a
            // conventional quadratic equation
            float a = gravity.sqrMagnitude;
            float b = -4 * (Vector3.Dot(gravity, delta) + muzzleVelocity * muzzleVelocity);
            float c = 4 * delta.sqrMagnitude;

            // check for no real solutions
            float b24ac = b * b - 4 * a * c;
            if (b24ac < 0)
            {
                path = null;
                return;
            }
            
            // find the candidate times
            float t1 = (-b + Mathf.Sqrt(b24ac)) / (2 * a);
            float t2 = (-b - Mathf.Sqrt(b24ac)) / (2 * a);
            
            // pick the best candidate
            float t = longerArc ? Mathf.Max(t1, t2) : Mathf.Min(t1, t2);
            if (t < 0)
            {
                t = Mathf.Max(t1, t2);
            }
            if (t < 0)
            {
                path = null;
                return;
            }
            
            // calculate the firing direction
            Vector3 firing = (delta * 2 - gravity * (t * t)) / (2 * muzzleVelocity * t);
            
            path = new Vector3[pathSteps];
            for (int i = 0; i<pathSteps; ++i)
            {
                float time = t * i / (pathSteps - 1);
                path[i] = start + muzzleVelocity * time * firing + gravity * (time * time) / 2;
            }
        }
    }
}