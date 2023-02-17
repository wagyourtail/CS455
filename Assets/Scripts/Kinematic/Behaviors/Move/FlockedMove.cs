using UnityEngine;

namespace Behaviors.Move
{
    public class FlockedMove : BaseMoveBehavior
    {
        private int[] weights;
        private BaseMoveBehavior[] behaviors;

        public FlockedMove(BaseMoveBehavior[] behaviors, int[] weights) : base(behaviors[0].character)
        {
            this.weights = weights;
            this.behaviors = behaviors;
        }
        
        public override Vector3? UpdateVelocity()
        {
            int totalWeight = 0;
            Vector3 target = Vector3.zero;
            for (int i = 0; i < behaviors.Length; i++)
            {
                Vector3? velocity = behaviors[i].UpdateVelocity();
                if (velocity.HasValue)
                {
                    target += velocity.Value * weights[i];
                    totalWeight += weights[i];
                }
            }
            if (totalWeight == 0) return null;
            return target / totalWeight;
        }
    }
}