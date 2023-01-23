using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public static bool shouldFollowPlayer = true;
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
        if (shouldFollowPlayer)
        {
            Kinematic.KinematicSteeringOutput output = Kinematic.Seek(transform, player.transform, 3);
            transform.position += output.velocity * Time.deltaTime;
            Vector3 euler = transform.eulerAngles;
            transform.eulerAngles = new Vector3(euler.x, output.rotation, euler.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Score.Get().addPoint();
        disable();
    }

    private void disable()
    {
        passed = true;
        gameObject.SetActive(false);
    }
}
