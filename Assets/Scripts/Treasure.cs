using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Treasure : MonoBehaviour
{
        
    public bool willExplode = false;
    public bool isHostile = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            willExplode = !willExplode;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            isHostile = !isHostile;
            return;
        }
    }
}
