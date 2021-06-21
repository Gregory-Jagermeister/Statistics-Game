using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip clapping;
    public AudioClip doorOpening;

    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "clapping":
                audioSource.PlayOneShot(clapping);
                break;
            case "doorOpening":
                audioSource.PlayOneShot(doorOpening);
                break;
        }

    }
}
