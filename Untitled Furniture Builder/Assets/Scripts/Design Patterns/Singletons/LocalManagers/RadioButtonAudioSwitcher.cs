﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioButtonAudioSwitcher : MonoBehaviour
{
    GameManager _instance;
    private Color[] color_debug_log = new Color[]{    Color.red,
                                                     Color.green,
                                                     Color.blue,
                                                     Color.magenta};

    [SerializeField] AudioClip _radioChangeEffect;
    private void Start()
    {
        _instance = GameManager.Instance;
    }
    private void OnMouseDown()
    {
        Color color = color_debug_log[1];
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f),
                                    "Before Stopping music"));
        _instance.AudioManager.AudioSourceBackgroundMusic.Stop();

        float _bufferVolume = _instance.AudioManager.AudioSourceBackgroundMusic.volume;

        _instance.AudioManager.AudioSourceBackgroundMusic.volume = 1f;
        _instance.AudioManager.AudioSourceBackgroundMusic.PlayOneShot(_radioChangeEffect);

        _instance.AudioManager.AudioSourceBackgroundMusic.volume = _bufferVolume;
        _instance.AudioManager.PlayBGMusic();
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f),
                                    "After Playing music"));
    }
}
