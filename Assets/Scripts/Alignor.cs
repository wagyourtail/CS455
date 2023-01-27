using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alignor : Kinematic
{
    Align<Alignor> myRotateType;

    // Start is called before the first frame update
    void Start()
    {

        myRotateType = new Align<Alignor>();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
