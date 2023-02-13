using UnityEngine;

namespace Behaviors.Move
{
    public class Seperation : Flee
    {
        public float threshold = 5.0f;

        public float decay = 100f;
        
        public Seperation(BaseKinematic character) : base(character)
        {
        }
        
        // TODO: multi-seperation and other move behaviors with these
        // private GameObject GetClosest()
        // {
        //     GameObject closest = null;
        //     float closestDistance = float.MaxValue;
        //     foreach (var target in targets)
        //     {
        //         var distance = Vector3.Distance(character.transform.position, target.transform.position);
        //         if (distance < closestDistance)
        //         {
        //             closest = target;
        //             closestDistance = distance;
        //         }
        //     }
        //
        //     return closest;
        // }

        // public override Vector3? GetTargetPosition()
        // {
        //     target = GetClosest();
        //     return base.GetTargetPosition();
        // }

        public override Vector3? UpdateVelocity()
        {
            Vector3? targetPosition = GetTargetPosition();
            if (targetPosition.HasValue)
            {
                float distance = targetPosition.Value.magnitude;
                if (distance < threshold)
                {
                    
                    float strength = Mathf.Min(decay / (distance * distance), maxAcceleration);
                    return (targetPosition.Value.normalized * strength) - character.linearVelocity;
                }
            }

            return null;
        }
    }
}