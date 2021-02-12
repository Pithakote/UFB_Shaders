using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleMusic : ToggleAudio
{
    protected override void Start()
    {
        base.Start();
        _audioSrc = _instance.AudioManager.AudioSourceBackgroundMusic;
        GetComponent<Toggle>().isOn = !_audioSrc.mute;
    }
}
