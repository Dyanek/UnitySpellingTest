﻿using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle (AudioClip clip)
    {
        efxSource.PlayOneShot(clip);
    }

    public void PlayDeadAudio(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }
}
