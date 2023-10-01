using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoskitoCollisionScript : MonoBehaviour,Shootable
{
    MoskitoScript moskito;
    private int hp = 5;
    private AudioSource hit;

    public void Shot(int damage, bool enemy)
    {
        hit.Play();
        hp -= damage;
        if (hp <= 0) moskito.DesOrbit();
    }

    // Start is called before the first frame update
    void Start()
    {
        moskito = transform.parent.GetComponent<MoskitoScript>();

        AudioSource[] audios = GetComponents<AudioSource>();
        hit = audios[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
