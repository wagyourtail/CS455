using UnityEngine;

namespace Behaviors.Move
{
    public class Seek : BaseMoveBehavior
    {
        public GameObject target;
        
        public Seek(BaseKinematic character) : base(character)
        {
        }

        /**
         * @return the relative position of the target
         */
        public virtual Vector3? GetTargetPosition()
        {
            if (target == null) return null;
            return target.transform.position - character.transform.position;
        }

        public override Vector3? UpdateVelocity()
        {
            Vector3? targetPosition = GetTargetPosition();
            if (targetPosition.HasValue)
            {
                if (targetPosition.Value == Vector3.positiveInfinity)
                {
                    return null;
                }
                
                Vector3 desiredVelocity = targetPosition.Value.normalized * maxVelocity;
                return desiredVelocity - character.linearVelocity;
            }
            return null;
        }
    }
}