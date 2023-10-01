using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript : MonoBehaviour
{
    public static OrbitScript current;
    public List<Transform> Moskitos;
    float rotation;

    float shootCd = 0.75f, shootTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Moskitos = new List<Transform>();
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        rotation += Time.deltaTime * 70f;
        var count = Moskitos.Count;
        if (count == 0) return;
        var step = 360f / count;
        var i = 0;
        foreach (Transform moskito in Moskitos)
        {
            moskito.localRotation = Quaternion.Euler(0,0,rotation + step * i);
            i++;
        }

        if (shootTime > shootCd)
        {
            shootTime = 0;
            if (Random.Range(1, 11) <= Moskitos.Count)
                Moskitos[Random.Range(0, Moskitos.Count)].GetComponent<MoskitoScript>().Shoot();
            return;
        }
        shootTime += Time.deltaTime;

    }
}
