using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called before the first frame update

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        Save();
    }

    public void Load()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
    
}
