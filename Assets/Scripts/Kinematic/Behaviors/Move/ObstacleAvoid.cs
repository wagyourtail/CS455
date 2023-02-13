using UnityEditor.PackageManager;
using UnityEngine;

namespace Behaviors.Move
{
    public class ObstacleAvoid : Seek
    {
        public float collisionRadius = 3.0f;
        public float lookAheadDist = 10.0f;
        public float avoidDist = 10.0f;
        
        public ObstacleAvoid(BaseKinematic character) : base(character)
        {
        }

        public override Vector3? GetTargetPosition()
        {
            RaycastHit hit;
            if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookAheadDist))
            {
                // Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * hit.distance, Color.red, 0.5f);
                Vector3 hitNormal = hit.normal;
                
                // HACKFIX: make sure avoid doesn't try to go over/under
                hitNormal.y = 0;
                Vector3 hitPos = hit.point;
                hitPos.y = character.transform.position.y;
                
                // draw normal
                // Debug.DrawRay(hitPos, hitNormal * avoidDist, Color.magenta, 0.5f);
                
                return (hitPos + hitNormal * avoidDist) - character.transform.position;
            }
            // Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * lookAheadDist, Color.green, 0.5f);
            return null;
        }
    }
}