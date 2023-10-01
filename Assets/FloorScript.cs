using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public static FloorScript current;
    public LineScript[] lines;
    public List<List<FloortileScript>> tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<List<FloortileScript>>();
        current = this;
        lines = GetComponentsInChildren<LineScript>();
        foreach (LineScript line in lines)
        {
            tiles.Add(line.floortiles.Cast<FloortileScript>().ToList());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
