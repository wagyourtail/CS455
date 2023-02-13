using UnityEngine;

namespace Behaviors.Look
{
    public class Face : BaseLookBehavior
    {
        private GameObject target;

        public Face(BaseKinematic character) : base(character)
        {
        }

        public override float? GetTargetAngle()
        {
            if (target == null)
            {
                return null;
            }
            Vector3 vec = target.transform.position - character.transform.position;
            return Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;
        }
    }
}