using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip backgroundClip;
    public float volume = 0.1f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }
}
