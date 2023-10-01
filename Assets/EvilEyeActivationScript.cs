using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilEyeActivationScript : MonoBehaviour
{
    public static EvilEyeActivationScript current;

    EvilEyeScript[] evilEyes;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        evilEyes = GetComponentsInChildren<EvilEyeScript>();
        //evilEyes[3].Show();
    }

    public void Activate(int number)
    {
        evilEyes[number].Show();
    }
}
