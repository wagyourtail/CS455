using UnityEngine;

public abstract class BaseKinematic : MonoBehaviour
{
    public Vector3 linearVelocity;
    public float angularVelocity;  // Millington calls this rotation
    // because I'm attached to a gameobject, we also have:
    // rotation <<< Millington calls this orientation
    // position
    public float maxSpeed = 10.0f;
    public float maxAngularVelocity = 45.0f; // degrees

    protected void Update()
    {
        // something is breaking my angular velocity
        // check here and reset it if it broke
        if (float.IsNaN(angularVelocity))
        {
            angularVelocity = 0.0f;
        }
        
        // update my position and rotation - Millington p. 58, lines 7-9
        this.transform.position += linearVelocity * Time.deltaTime;


        if (Mathf.Abs(angularVelocity) > 0.01f)
        {
            Vector3 v = new Vector3(0, angularVelocity, 0);
            this.transform.eulerAngles += v * Time.deltaTime;
        }

        // update linear and angular velocities - I might be accelerating or decelerating, etc.
        // Millington p. 58, lines 11-13
        SteeringOutput? steeringUpdate = UpdateSteering();
        
        if (steeringUpdate.HasValue)
        {
            linearVelocity += steeringUpdate.Value.linear * Time.deltaTime;
            angularVelocity += steeringUpdate.Value.angular * Time.deltaTime;
        }

        // check for speeding and clip
        // Millington p.58, lines 15-18
        // note that Millington's pseudocode on p.58 does not clip angular velocity, but we do here
        if (linearVelocity.magnitude > maxSpeed)
        {
            linearVelocity.Normalize();
            linearVelocity *= maxSpeed;
        }
        if (Mathf.Abs(angularVelocity) > maxAngularVelocity)
        {
            angularVelocity = maxAngularVelocity * (angularVelocity / Mathf.Abs(angularVelocity));
        }
    }
    
    
    protected abstract SteeringOutput? UpdateSteering();

    public struct SteeringOutput {
        public Vector3 linear;
        public float angular; // degrees please
    }

}
