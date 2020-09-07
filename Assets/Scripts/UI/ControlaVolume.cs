using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlaVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    float volumeMaster, volumeSfx, volumeMusic;

    private void Start()
    {
        
    }

    public void SetVolumeMaster(float sliderValue)
    {
        volumeMaster = Mathf.Log10(sliderValue)*20;
        audioMixer.SetFloat("MasterVolume", volumeMaster);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void SetVolumeSfx(float sliderValue)
    {
        volumeSfx = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("SfxVolume", volumeSfx);
        PlayerPrefs.SetFloat("SfxVolume", sliderValue);
    }

    public void SetVolumeMusic(float sliderValue)
    {
        volumeMusic = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("MusicVolume", volumeMusic);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}
