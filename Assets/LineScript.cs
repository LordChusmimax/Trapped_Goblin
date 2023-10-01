using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public FloortileScript[] floortiles;
    // Start is called before the first frame update
    void Start()
    {
        floortiles = GetComponentsInChildren<FloortileScript>();
        short count = 0;
        foreach (FloortileScript tile in floortiles)
        {
            tile.column = count;
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
