using UnityEngine;

namespace Behaviors.Move
{
    public class Pursue : Seek
    {
        public float maxPredTime = 1f;

        public Pursue(BaseKinematic character) : base(character)
        {
        }
        
        public override Vector3? GetTargetPosition()
        {
            if (target == null) return null;
            // 1. figure out how far ahead in time we should predict
            Vector3 directionToTarget = target.transform.position - character.transform.position;
            float distanceToTarget = directionToTarget.magnitude;
            float mySpeed = character.linearVelocity.magnitude;
            float predictionTime; 
            if (mySpeed <= distanceToTarget / maxPredTime)
            {
                // if I'm far enough away, I can use the max prediction time
                predictionTime = maxPredTime;
            }
            else
            {
                // if I'm close enough that my current speed will get me to 
                // the target before the max prediction time elapses
                // use a smaller prediction time
                predictionTime = distanceToTarget / mySpeed;
            }

            // 2. get the current velocity of our target and add an offset based on our prediction time
            //Kinematic myMovingTarget = target.GetComponent(typeof(Kinematic)) as Kinematic;
            BaseKinematic myMovingTarget = target.GetComponent<BaseKinematic>();
            if (myMovingTarget == null)
            {
                // default to seek behavior for non-kinematic targets
                return base.GetTargetPosition();
            }

            return target.transform.position + myMovingTarget.linearVelocity * predictionTime - character.transform.position;
        }
    }
}