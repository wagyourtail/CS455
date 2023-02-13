using UnityEngine;

namespace Behaviors.Move
{
    public class Arrive : Seek
    {
        
        // the radius for arriving at the target
        public float targetRadius = 1.5f;

        // the radius for beginning to slow down
        public float slowRadius = 3f;

        // the time over which to achieve target speed
        public float timeToTarget = 0.1f;
        
        
        public Arrive(BaseKinematic character) : base(character)
        {
        }

        public override Vector3? UpdateVelocity()
        {
            Vector3? targetPos = GetTargetPosition();
            if (targetPos.HasValue)
            {
                // get the direction to the target
                float distance = targetPos.Value.magnitude;

                if (distance < targetRadius)
                {
                    return null;
                }

                float targetSpeed;
                if (distance > slowRadius)
                {
                    targetSpeed = maxVelocity;
                }
                else
                {
                    targetSpeed = maxVelocity * distance / slowRadius;
                }

                Vector3 targetVelocity = targetPos.Value;
                targetVelocity.Normalize();
                targetVelocity *= targetSpeed;

                targetVelocity = (targetVelocity - character.linearVelocity) / timeToTarget;

                if (targetVelocity.magnitude > maxAcceleration)
                {
                    targetVelocity.Normalize();
                    targetVelocity *= maxAcceleration;
                }

                return targetVelocity;
            }
            return null;
        }
    }
}