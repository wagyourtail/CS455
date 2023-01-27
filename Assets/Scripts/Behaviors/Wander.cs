using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face<Wanderer>
{
    public float wanderOffset = 4f;
    public float wanderRadius = 2f;
    
    public float wanderRate = 4f;

    private float wanderOrientation = 10f;
    
    
    public override SteeringOutput getSteering()
    {
        wanderOrientation += Random.Range(-1, 1) * wanderRate;
        target.transform.rotation = Quaternion.Euler(new Vector3(0, wanderOrientation + character.transform.eulerAngles.y, 0));
        target.transform.position = character.transform.position + wanderOffset * (character.transform.rotation * Vector3.forward);
        target.transform.position += wanderRadius * (target.transform.rotation * Vector3.forward);

        SteeringOutput output = base.getSteering();
        output.linear = (target.transform.position - character.transform.position).normalized * character.maxSpeed; 
        return output;
    }
}
