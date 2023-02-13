using UnityEngine;

namespace Behaviors.Move
{
    public class Flee : Seek
    {
        public Flee(BaseKinematic character) : base(character)
        {
        }

        public override Vector3? GetTargetPosition()
        {
            Vector3? targetPosition = base.GetTargetPosition();
            if (targetPosition.HasValue)
            {
                return -targetPosition.Value;
            }
            return null;
        }
    }
}