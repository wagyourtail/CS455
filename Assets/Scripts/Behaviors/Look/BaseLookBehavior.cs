using UnityEngine;

namespace Behaviors.Look
{
    public abstract class BaseLookBehavior
    {

        protected readonly BaseKinematic character;
        public float maxAngularVelocity = 45.0f;
        
        public float maxAngularAccel = 1f;
        public float slowRadius = 10f;
        
        public float timeToTarget = 0.1f;

        public BaseLookBehavior(BaseKinematic character)
        {
            this.character = character;
        }

        public abstract float? GetTargetAngle();
        
        public virtual float? UpdateLookDirection()
        {
            float? target = GetTargetAngle();
            if (target.HasValue)
            {
                float rotation = Mathf.DeltaAngle(character.transform.eulerAngles.y, target.Value);
                float rotationSize = Mathf.Abs(rotation);
                
                float targetRotation = 0;
                if (rotationSize < slowRadius)
                {
                    targetRotation = maxAngularAccel * rotationSize / slowRadius;
                }
                else
                {
                    targetRotation = maxAngularAccel;
                }
                
                targetRotation *= rotation / rotationSize;
                
                float currentAngularVelocity = float.IsNaN(character.angularVelocity) ? 0 : character.angularVelocity;
                float result = (targetRotation - currentAngularVelocity) / timeToTarget;
                
                float angularAccel = Mathf.Abs(result);
                if (angularAccel > maxAngularAccel)
                {
                    result = result / angularAccel * maxAngularAccel;
                }

                return result;
            }
            return null;
            
        }
    }
}