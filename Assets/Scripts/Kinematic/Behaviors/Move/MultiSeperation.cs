using UnityEngine;

namespace Behaviors.Move
{
    public class MultiSeperation : Seperation
    {
        public GameObject[] targets;
        
        public MultiSeperation(BaseKinematic character) : base(character)
        {
        }
        
        private GameObject GetClosest()
        {
            GameObject closest = null;
            float closestDistance = float.MaxValue;
            foreach (var target in targets)
            {
                var distance = Vector3.Distance(character.transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closest = target;
                    closestDistance = distance;
                }
            }
        
            return closest;
        }

        public override Vector3? GetTargetPosition()
        {
            target = GetClosest();
            return base.GetTargetPosition();
        }
    }
}