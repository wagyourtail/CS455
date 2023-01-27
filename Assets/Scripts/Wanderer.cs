using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Kinematic
{
    Wander wander;

    // Start is called before the first frame update
    void Start()
    {
        wander = new Wander();
        wander.character = this;
        wander.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = wander.getSteering();
        base.Update();
    }
}
