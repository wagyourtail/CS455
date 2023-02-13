using UnityEngine;

namespace Behaviors.Move
{
    public class Wander : Seek
    {
        
        public float wanderOffset = 4f;
        public float wanderRadius = 2f;
    
        public float wanderRate = 4f;

        private float wanderOrientation = 10f;
        
        private float wanderDirection = 0f;
        
        public Wander(BaseKinematic character) : base(character)
        {
        }
        
        public override Vector3? GetTargetPosition()
        {
            wanderOrientation += Random.Range(0f, 1f) * wanderRate;
            var targetRot = Quaternion.Euler(new Vector3(0, wanderOrientation + wanderDirection, 0));
            var charPos = character.transform.position;
            var targetPos = charPos + wanderOffset * (character.transform.rotation * Vector3.forward);
            targetPos += wanderRadius * (targetRot * Vector3.forward);
            Debug.DrawLine(charPos, targetPos, Color.blue);
            Vector3 dir = targetPos - charPos;
            wanderDirection = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            return dir;
        }
    }
}