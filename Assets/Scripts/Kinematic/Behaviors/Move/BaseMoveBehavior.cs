using UnityEngine;

namespace Behaviors.Move
{
    public abstract class BaseMoveBehavior
    {
        public readonly BaseKinematic character;
        
        public float maxAcceleration = 100f;
        public float maxVelocity = 10.0f;
        
        public BaseMoveBehavior(BaseKinematic character)
        {
            this.character = character;
            this.maxVelocity = this.character.maxSpeed;
        }

        public abstract Vector3? UpdateVelocity();
    }
}