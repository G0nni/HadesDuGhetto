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
        // Check si le joueur a déjà défini le volume
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            // Charge le volume
            LoadVolume();
        }
        else
        {
            // Définit le volume par défaut
            SetVolume();
        }
    }

    // Définit le volume
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
