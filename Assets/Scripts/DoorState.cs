using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorState : MonoBehaviour
{
    public Door door;
    public Treasure treasure;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        String s = "Door: ";
        if (door.isClosed)
        {
            s += "Closed";
        }
        else
        {
            s += "Open";
        }

        if (door.isLocked)
        {
            s += " Locked";
        }

        s += "\n";
        s += "Treasure: ";
        if (treasure.willExplode)
        {
            s+= "Explodes";
        }
        
        if (treasure.isHostile)
        {
            s+= " Hostile";
        }

        text.text = s;
    }
}
