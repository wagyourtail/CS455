using UnityEngine;

namespace Behaviors.Move
{
    public class CollisionAvoid : BaseMoveBehavior
    {
        public BaseKinematic[] targets;
        
        public float collisionRadius = 1.0f;
        public float lookAheadDist = 10.0f;
        
        public CollisionAvoid(BaseKinematic character) : base(character)
        {
        }

        public override Vector3? UpdateVelocity()
        {
            float shortestTime = float.PositiveInfinity;

            BaseKinematic firstTarget = null;
            float firstMinSep = float.PositiveInfinity;
            float firstDist = float.PositiveInfinity;
            
            Vector3 firstRelativePos = Vector3.zero;
            Vector3 firstRelativeVel = Vector3.zero;
            
            Vector3 relativePos;

            foreach (var target in targets)
            {
                relativePos = target.transform.position - character.transform.position;
                Vector3 relativeVel = character.linearVelocity - target.linearVelocity;
                float relativeSpeed = relativeVel.magnitude;
                float timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);
                
                float dist = relativePos.magnitude;
                float minSep = dist - relativeSpeed * timeToCollision;

                if (minSep > 2 * collisionRadius)
                {
                    continue;
                }

                if (timeToCollision > 0 && timeToCollision < shortestTime)
                {
                    shortestTime = timeToCollision;
                    firstTarget = target;
                    firstMinSep = minSep;
                    firstDist = dist;
                    firstRelativePos = relativePos;
                    firstRelativeVel = relativeVel;
                }
            }

            // no target, or out of range
            if (firstTarget == null) return null;
            if (firstRelativePos.magnitude > lookAheadDist) return null;

            float dotResult = Vector3.Dot(character.linearVelocity.normalized, firstTarget.linearVelocity.normalized);
            Vector3 result;
            if (dotResult < 0.9)
            {
                // head-on
                result = new Vector3(character.linearVelocity.z, 0.0f, character.linearVelocity.x);
            }
            else
            {
                result = -firstTarget.linearVelocity;
            }

            return result - character.linearVelocity;
        }
    }
}