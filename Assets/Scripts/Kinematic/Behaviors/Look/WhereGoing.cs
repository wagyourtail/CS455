using UnityEngine;

namespace Behaviors.Look
{
    public class WhereGoing : BaseLookBehavior
    {
        public WhereGoing(BaseKinematic character) : base(character)
        {
        }

        public override float? GetTargetAngle()
        {
            Vector3 velocity = character.linearVelocity;
            if (velocity.magnitude == 0) return null;
            
            return Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        }
    }
}