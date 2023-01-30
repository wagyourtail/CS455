using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pather : Kinematic
{
    FollowPath myMoveType;
    Face<Pather> mySeekRotateType;
    
    public GameObject[] path;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new FollowPath();
        myMoveType.character = this;
        myMoveType.target = myTarget;
        myMoveType.flee = false;
        Vector3[] pathVector = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            pathVector[i] = path[i].transform.position;
        }
        myMoveType.path = pathVector;

        mySeekRotateType = new Face<Pather>();
        mySeekRotateType.character = this;
        mySeekRotateType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = mySeekRotateType.getSteering().angular;
        base.Update();
    }
}