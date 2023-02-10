using UnityEngine;

public class ObstacleAvoider : Seek
{
    
    public float maxAcceleration = 1.0f;
    public float collisionRadius = 1.0f;
    public float lookAheadDist = 10.0f;
    public float avoidDist = 3.0f;

    private Vector3? targetPos = null;

    private void updateTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookAheadDist))
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * hit.distance, Color.red, 0.5f);
            // don't allow y axis movement
            Vector3 hitNormal = hit.normal;
            hitNormal.y = 0;

            targetPos = hit.point + (hitNormal * avoidDist);
        }
        else
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * lookAheadDist, Color.green, 0.5f);
            targetPos = null;
        }
    }
        
    protected override Vector3 getTargetPosition()
    {
        return targetPos.Value;
    }

    public override SteeringOutput getSteering()
    {
        updateTarget();
        if (targetPos.HasValue)
        {
            return base.getSteering();
        }
        return null;
    }
}