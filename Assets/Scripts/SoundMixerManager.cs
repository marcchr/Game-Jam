using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider soundFXSlider;
    [SerializeField] Slider musicSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("masterLevel"))
        {
            PlayerPrefs.SetFloat("masterLevel", 1);
            LoadMaster();
        }
        else
        {
            LoadMaster();
        }

        if (!PlayerPrefs.HasKey("soundFXLevel"))
        {
            PlayerPrefs.SetFloat("soundFXLevel", 1);
            LoadSoundFX();
        }
        else
        {
            LoadSoundFX();
        }

        if (!PlayerPrefs.HasKey("musicLevel"))
        {
            PlayerPrefs.SetFloat("musicLevel", 1);
            LoadMusic();
        }
        else
        {
            LoadMusic();
        }
    }
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        SaveMaster();
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        SaveSoundFX();

    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        SaveMusic();

    }

    private void LoadMaster()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterLevel");
    }

    public void SaveMaster()
    {
        PlayerPrefs.SetFloat("masterLevel", masterSlider.value);
    }

    private void LoadSoundFX()
    {
        soundFXSlider.value = PlayerPrefs.GetFloat("soundFXLevel");
    }

    public void SaveSoundFX()
    {
        PlayerPrefs.SetFloat("soundFXLevel", soundFXSlider.value);
    }
    private void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicLevel");
    }

    public void SaveMusic()
    {
        PlayerPrefs.SetFloat("musicLevel", musicSlider.value);
    }
}
