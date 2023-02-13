using Behaviors.Look;
using Behaviors.Move;
using UnityEngine;

public class CatKinematic : LayeredKinematic
{

    public GameObject dog;
    
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
                new Seperation(this)
                {
                    target = dog
                }
            ),
            new KinematicLayer(
                new WhereGoing(this),
                new Wander(this)
                {
                    wanderRadius = 4f,
                    wanderOffset = 3f
                }
            )
        };
    }
}
