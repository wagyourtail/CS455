
using Behaviors.Look;
using Behaviors.Move;
using UnityEngine;

public class LayeredKinematic : BaseKinematic
{
    public KinematicLayer[] layers;
    
    protected override SteeringOutput? UpdateSteering()
    {
        foreach (var layer in layers)
        {
            SteeringOutput? steering = layer.UpdateSteering();
            if (steering.HasValue)
            {
                return steering;
            }
        }
        return null;
    }

    public class KinematicLayer
    {
        public BaseLookBehavior lookBehavior;
        public BaseMoveBehavior moveBehavior;

        public KinematicLayer(BaseLookBehavior lookBehavior, BaseMoveBehavior moveBehavior)
        {
            this.lookBehavior = lookBehavior;
            this.moveBehavior = moveBehavior;
        }
        
        public SteeringOutput? UpdateSteering()
        {
            SteeringOutput steering = new SteeringOutput();
            if (lookBehavior != null)
            {
                float? angular = lookBehavior.UpdateLookDirection();
                if (angular.HasValue)
                {
                    steering.angular = angular.Value;
                }
                else
                {
                    return null;
                }
            }
            if (moveBehavior != null)
            {
                Vector3? velocity = moveBehavior.UpdateVelocity();
                if (velocity.HasValue)
                {
                    steering.linear = velocity.Value;
                }
                else
                {
                    return null;
                }
            }
            
            return steering;
        }


    }
}