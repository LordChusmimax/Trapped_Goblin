using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    AudioSource audio;
    [SerializeField]AudioClip music;

    public static MusicManagerScript current;
    public static bool on = false;
    // Start is called before the first frame update

    private void Start()
    {
        if (on)
        {
            Destroy(gameObject);
        }
        on = true;
        audio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        audio.clip = music;
        audio.Play();
    }
    // Update is called once per frame
    void Update()
    {
 
    }
}
