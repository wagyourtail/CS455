using UnityEngine;

namespace Behaviors.Move
{
    public abstract class BaseMoveBehavior
    {
        protected readonly BaseKinematic character;
        
        public float maxAcceleration = 1f;
        public float maxVelocity = 10.0f;
        
        public BaseMoveBehavior(BaseKinematic character)
        {
            this.character = character;
        }

        public abstract Vector3? UpdateVelocity();
    }
}