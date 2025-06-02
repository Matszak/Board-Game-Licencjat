using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsSound : MonoBehaviour
{
    public void PlaySound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayButtonSound();
        }
        else
        {
            Debug.LogWarning("Audio Manager is null!");
        }
    }
}
