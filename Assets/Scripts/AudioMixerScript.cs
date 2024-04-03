using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerScript : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider mySlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
    }

    public void SetVolume()
    {
        float volume = mySlider.value;
        myMixer.SetFloat("Volume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void LoadVolume()
    {
        mySlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetVolume();
    }
}
