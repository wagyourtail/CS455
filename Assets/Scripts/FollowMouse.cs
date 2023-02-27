using System;
using Behaviors.Look;
using Behaviors.Move;
using UnityEngine;

public class FollowMouse : LayeredKinematic
{
    public GameObject mouseTarget;
    
    private BallisticArc ballisticArc;
    
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
                    longerArc = true
                }
            )
        };
    }
    
    bool debounce = false;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (debounce) return;
            Debug.Log("OnMouseDown");
            ballisticArc.UpdateTarget(mouseTarget.transform.position);
            debounce = true;
        } else
        {
            debounce = false;
        }
    }
}
