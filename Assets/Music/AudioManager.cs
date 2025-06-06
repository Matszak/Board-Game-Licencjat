using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    
    [Header("Audio Clips")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip buttonSound;
    [SerializeField] AudioClip rollSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip fightSound;

    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;
    
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
     float musicVolume = PlayerPrefs.GetFloat("musicVolume");
     float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

     mixer.SetFloat("music", Mathf.Log10
         (Mathf.Clamp(musicVolume, 0.0001f, 1f)) * 20);
     mixer.SetFloat("SFX", Mathf.Log10
         (Mathf.Clamp(sfxVolume, 0.0001f, 1f)) * 20);

     musicSource.clip = backgroundMusic; 
     musicSource.Play();
    }

    public void PlayButtonSound()
    {
        sfxSource.PlayOneShot(buttonSound);
    }

    public void PlayRollDiceSound() => sfxSource.PlayOneShot(rollSound);

    internal void PlayJumpSound() => sfxSource.PlayOneShot(jumpSound);
    internal void PlayFightSound() => sfxSource.PlayOneShot(fightSound);
}
