using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElusiveBehavior : MonoBehaviour
{
    bool passed = false;

    private void FixedUpdate()
    {
        if (passed) return;
        GameObject player = GameObject.Find("Player"); 
        if (player.transform.position.z > transform.position.z)
        {
            passed = true;
            return;
        }
        Kinematic.KinematicSteeringOutput output = Kinematic.Flee(transform, player.transform, 3);
        transform.position += output.velocity * Time.deltaTime;
        Vector3 euler = transform.eulerAngles; 
        transform.eulerAngles = new Vector3(euler.x, output.rotation, euler.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        Score.Get().addPoints(5);
        disable();
    }

    private void disable()
    {
        passed = true;
        gameObject.SetActive(false);
    }
}
