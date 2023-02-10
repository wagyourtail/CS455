using UnityEngine;

public class CollisionAvoider : LookWhereGoing
{
    public Kinematic[] targets;
    
    public float collisionRadius = 1.0f;
    public float maxAcceleration = 1.0f;
    public float lookAheadDist = 10.0f;
    
    public override SteeringOutput getSteering()
    {
        float shortestTime = float.PositiveInfinity;
        
        Kinematic firstTarget = null;
        float firstMinSeparation = float.PositiveInfinity;
        float firstDistance = float.PositiveInfinity;
        Vector3 firstRelativePos = Vector3.zero;
        Vector3 firstRelativeVel = Vector3.zero;
        
        Vector3 relativePos;
        foreach (Kinematic target in targets)
        {
            relativePos = target.transform.position - character.transform.position;
            // Vector3 relativeVel = target.linearVelocity - character.linearVelocity;
            Vector3 relativeVel = character.linearVelocity - target.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);
            
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;
            
            if (minSeparation > 2 * collisionRadius)
                continue;
            
            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }
        
        

        if (firstTarget == null)
            return null;

        // max distance check, so it's not just when they "see" eachother
        if (firstRelativePos.magnitude > lookAheadDist)
            return null;
        
        // if (firstMinSeparation <= 0 || firstDistance < 2 * collisionRadius)
        // {
        //     relativePos = firstTarget.transform.position - character.transform.position;
        // }
        // else
        // {
        //     relativePos = firstRelativePos + firstRelativeVel * shortestTime;
        // }
        
        
        // relativePos.Normalize();
        
        SteeringOutput result = new SteeringOutput();
        // result.linear = relativePos * character.maxSpeed;
        
        // check for a head-on collision
        float dotResult = Vector3.Dot(character.linearVelocity.normalized, firstTarget.linearVelocity.normalized);
        if (dotResult < -0.9)
        {
            // if we have an impending head-on collision. veer sideways
            //result.linear = -firstTarget.transform.right; // wrong
            result.linear = new Vector3(character.linearVelocity.z, 0.0f, character.linearVelocity.x);
        }
        else
        {
            // otherwise, steer to pass behind our moving target
            result.linear = -firstTarget.linearVelocity;
        }
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        
        result.angular = getTargetAngle();
        return result;
    }
}