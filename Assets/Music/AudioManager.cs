using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    
    public AudioManager()
    {
        
    }
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

        GameManager.Instance.OnFightStarted += InstanceOnOnFightStarted;
    }

    private void InstanceOnOnFightStarted(Player arg1, EnemyCard arg2)
    {
        PlayFightSound();
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

    public void PlayRollDiceSound() => sfxSource.PlayOneShot(rollSound);

    internal void PlayJumpSound() => sfxSource.PlayOneShot(jumpSound);
    internal void PlayFightSound() => sfxSource.PlayOneShot(fightSound);
}
