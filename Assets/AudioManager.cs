using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    
    [Header("Audio Clips")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip buttonSound;

    public static AudioManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayButtonSound()
    {
        sfxSource.PlayOneShot(buttonSound);
    }
}
