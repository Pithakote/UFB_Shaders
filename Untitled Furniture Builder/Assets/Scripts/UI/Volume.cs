using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer audio;
   
    public void MusicVolume (float volumeSlider)
    {
        audio.SetFloat("MusicVolume", Mathf.Log10(volumeSlider) * 20);
    }
}
