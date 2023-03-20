using System;
using System.Collections.Generic;
using Behaviors.Look;
using Behaviors.Move.Pathfinding;
using UnityEngine;

public class Pathfinder : LayeredKinematic
{

    public GameObject[] graph;

    public Dijkstra dijkstra;
    public bool useWeights = false;
    
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
                dijkstra = new Dijkstra(this)
                {
                    useWeights = useWeights
                }
            )
        };
        Dictionary<GameObject, List<GameObject>> g = new();

        for (int i = 0; i < graph.Length; i+=2)
        {
            var connection0 = graph[i];
            var connection1 = graph[i + 1];
            if (!g.ContainsKey(connection0))
            {
                g.Add(connection0, new List<GameObject> {connection1});
            }
            else
            {
                g[connection0].Add(connection1);
            }

            if (!g.ContainsKey(connection1))
            {
                g.Add(connection1, new List<GameObject> {connection0});
            }
            else
            {
                g[connection1].Add(connection0);
            }
        }
        dijkstra.addNodes(g);
    }

    private void Update()
    {
        base.Update();
        foreach (var node in dijkstra.getNodes())
        {
            foreach (var conn in node.neighbors)
            {
                Debug.DrawLine(node.position, conn.position, Color.red);
            }
        }
    }
}
