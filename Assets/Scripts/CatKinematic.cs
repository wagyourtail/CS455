using System.Linq;
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
            ) {epsilon = 0.1f},
            new KinematicLayer(
                new WhereGoing(this),
                new Seperation(this)
                {
                    target = dog
                }
            ) {epsilon = 0.1f},
            new KinematicLayer(
                new WhereGoing(this),
                new FlockedMove(
                    new BaseMoveBehavior[]{
                        new Wander(this)
                        {
                            wanderRadius = 4f,
                            wanderOffset = 3f
                        },
                        new MultiSeperation(this)
                        {
                            // a bit hacky xd
                            targets = FindObjectsOfType<CatKinematic>(false).ToList().Where(e => e != this).Select(e => e.gameObject).ToArray()
                        }
                    },
                    new int[]
                    {
                        1,
                        10
                    })
            )
        };
    }
}
