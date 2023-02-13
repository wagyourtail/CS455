using Behaviors.Look;
using Behaviors.Move;
using UnityEngine;

public class FollowMouse : LayeredKinematic
{
    public GameObject mouseTarget;
    
    void Start()
    {
        layers = new KinematicLayer[]
        {
            new KinematicLayer(
                new WhereGoing(this),
                new ObstacleAvoid(this)
            ),
            new KinematicLayer(
                new WhereGoing(this),
                new Seek(this)
                {
                    target = mouseTarget
                }
            )
        };
    }
}
