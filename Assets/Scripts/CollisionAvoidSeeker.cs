using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidSeeker : Kinematic
{
    Seek myMoveType;
    Face<CollisionAvoidSeeker> mySeekRotateType;
    CollisionAvoider myCollisionAvoider;

    public Kinematic[] avoids;
    
    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Seek();
        myMoveType.character = this;
        myMoveType.target = myTarget;
        myMoveType.flee = false;

        mySeekRotateType = new Face<CollisionAvoidSeeker>();
        mySeekRotateType.character = this;
        mySeekRotateType.target = myTarget;
        
        myCollisionAvoider = new CollisionAvoider();
        myCollisionAvoider.character = this;
        myCollisionAvoider.targets = avoids;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        SteeringOutput collisionAvoidSteering = myCollisionAvoider.getSteering();
        if (collisionAvoidSteering == null)
        {
            // update normally
            steeringUpdate.linear = myMoveType.getSteering().linear;
            steeringUpdate.angular = mySeekRotateType.getSteering().angular;
        }
        else
        {
            // update with collision avoidance
            steeringUpdate.linear = collisionAvoidSteering.linear;
            steeringUpdate.angular = collisionAvoidSteering.angular;
        }
        base.Update();
    }
}
