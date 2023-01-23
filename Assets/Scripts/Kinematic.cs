using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Kinematic
{
    
    public static KinematicSteeringOutput Seek(Transform self, Transform target, float maxSpeed)
    {
        Vector3 direction = target.position - self.position;
        direction.Normalize();
        direction *= maxSpeed;

        float targetAngle = vecOrientation(self.rotation.eulerAngles.y, direction);
        
        return new KinematicSteeringOutput(direction, targetAngle);
    }
    
    private static float vecOrientation(float currentOrientation, Vector3 velocity)
    {
        if (velocity.magnitude > 0)
        {
            return Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        }
        else
        {
            return currentOrientation;
        }
    }
    
    public static KinematicSteeringOutput Flee(Transform self, Transform target, float maxSpeed)
    {
        Vector3 direction = self.position - target.position;
        direction.Normalize();
        direction *= maxSpeed;

        float targetAngle = vecOrientation(self.rotation.eulerAngles.y, direction);
        
        return new KinematicSteeringOutput(direction, targetAngle);
    }
    
    public struct KinematicSteeringOutput
    {
        public Vector3 velocity;
        public float rotation;
        
        public KinematicSteeringOutput(Vector3 velocity, float rotation)
        {
            this.velocity = velocity;
            this.rotation = rotation;
        }
    }
}