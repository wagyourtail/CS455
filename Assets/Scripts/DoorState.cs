using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorState : MonoBehaviour
{
    public Door door;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        String s = "";
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

        text.text = s;
    }
}
