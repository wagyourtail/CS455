namespace Behaviors.Look
{
    public class FlockedLook : BaseLookBehavior
    {
        private int[] weights;
        private BaseLookBehavior[] behaviors;

        public FlockedLook(BaseLookBehavior[] behaviors, int[] weights) : base(behaviors[0].character)
        {
            this.weights = weights;
            this.behaviors = behaviors;
        }

        private float fixAngle(float angle)
        {
            while (angle > 180)
            {
                angle -= 360;
            } 
            while (angle < -180)
            {
                angle += 360;
            }
            return angle;
        }
        
        public override float? GetTargetAngle()
        {
            int totalWeight = 0;
            float target = 0;
            for (int i = 0; i < behaviors.Length; i++)
            {
                float? angle = behaviors[i].GetTargetAngle();
                if (angle.HasValue)
                {
                    target += fixAngle(angle.Value) * weights[i];
                    totalWeight += weights[i];
                }
            }
            if (totalWeight == 0) return null;
            return target / totalWeight;
        }
    }
}