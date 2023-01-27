using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facer : Kinematic
{
    Face<Facer> myRotateType;

    // Start is called before the first frame update
    void Start()
    {

        myRotateType = new Face<Facer>();
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
