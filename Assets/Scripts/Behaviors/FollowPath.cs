using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FollowPath : Seek
{
    // the maximum prediction time
    public Vector3[] path;

    private float pathOffset = 1f;
    
    public override SteeringOutput getSteering()
    {
        // get the character's current position along path
        Vector3 current = character.transform.position;
        float currentParam = 0f;
        float minDist = float.MaxValue;
        for (int i = 0; i < path.Length; i++)
        {
            float dist = Vector3.Distance(current, path[i]);
            if (dist < minDist)
            {
                minDist = dist;
                currentParam = i;
            }
        }

        int next = (int) currentParam + 1;
        if (next < path.Length)
        {
            currentParam += 1;
        }

        // get the position on the path
        Vector3 target = path[(int) currentParam];
        this.target.transform.position = target;
        
        // get the steering
        return base.getSteering();
    }
}
