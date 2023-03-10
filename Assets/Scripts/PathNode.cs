using System;
using UnityEngine;

public class PathNode : MonoBehaviour
{

    // bool debounce = false;
    //
    // private void FixedUpdate()
    // {
    //     if (Input.GetMouseButton(0))
    //     {
    //         if (debounce) return;
    //         Debug.Log("OnMouseDown");
    //         // find ball kinematic
    //         GameObject ball = GameObject.FindGameObjectsWithTag("Player")[0];
    //         Debug.Log(ball);
    //         ball.GetComponent<BallKinematic>().ballisticArc.UpdateTarget(transform.position);
    //         debounce = true;
    //     } else
    //     {
    //         debounce = false;
    //     }
    // }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        // find ball kinematic
        GameObject pathfinder = GameObject.FindGameObjectsWithTag("Player")[0];
        Debug.Log(pathfinder);
        pathfinder.GetComponent<Pathfinder>().dijkstra.PathTo(transform.position);
    }
}