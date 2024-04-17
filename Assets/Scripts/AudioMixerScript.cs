using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerScript : MonoBehaviour
{
    // Variables
    [Header("Audio")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider mySlider;

    private void Start()
    {
        // Check si le joueur a d�j� d�fini le volume
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            // Charge le volume
            LoadVolume();
        }
        else
        {
            // D�finit le volume par d�faut
            SetVolume();
        }
    }

    // D�finit le volume
    public void SetVolume()
    {
        float volume = mySlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    // Charge le volume
    public void LoadVolume()
    {
        mySlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetVolume();
    }
}
