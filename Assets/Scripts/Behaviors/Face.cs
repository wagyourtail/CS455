using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face<T> : Align<T> where T: Kinematic
{
    // TODO: override Align's getTargetAngle to face the target instead of matching it's orientation
    public override float getTargetAngle()
    {
        // vec of this -> target
        Vector3 vec = target.transform.position - character.transform.position;
        // angle of vec
        return Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;
    }
}
