using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToClicked : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // find player
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        
        // get navmesh agent
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        
        // transform position to world position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 worldPosition = hit.point;
        Debug.Log(worldPosition);
        agent.SetDestination(worldPosition);
    }
}
