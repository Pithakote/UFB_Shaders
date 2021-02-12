using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Volume : MonoBehaviour
{
    public AudioMixer audio;
    [SerializeField]
    AudioSource _audioSrc;

    private void Start()
    {
        _audioSrc = GameManager.Instance.AudioManager.AudioSourceBackgroundMusic;

        gameObject.GetComponent<Slider>().value = _audioSrc.volume;
    }

    public void MusicVolume (float volumeSlider)
    {
        // audio.SetFloat("MusicVolume", Mathf.Log10(volumeSlider) * 20);
        _audioSrc.volume = volumeSlider;
    }
}
