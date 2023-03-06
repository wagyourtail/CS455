using System;
using Behaviors.Look;
using Behaviors.Move;
using UnityEngine;

public class BallKinematic : LayeredKinematic
{
    public BallisticArc ballisticArc;
    
    void Start()
    {
        layers = new KinematicLayer[]
        {
            // new KinematicLayer(
            //     new WhereGoing(this),
            //     new ObstacleAvoid(this)
            // ),
            new KinematicLayer(
                new WhereGoing(this),
                ballisticArc = new BallisticArc(this)
                {
                    longerArc = false,
                    gravity = Vector3.down * 20f
                }
            )
        };
    }
    //
    // private int disable = -1;
    //
    // private void FixedUpdate()
    // {
    //     if (disable-- == 0)
    //     {
    //         this.enabled = false;
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.isKinematic = false;
        body.useGravity = true;
        body.AddForce(this.linearVelocity * body.mass * 5);
        this.enabled = false;
    }
}
