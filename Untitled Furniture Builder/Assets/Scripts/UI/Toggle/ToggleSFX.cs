using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleSFX : ToggleAudio
{
    protected override void Start()
    {
        base.Start();
        _audioSrc = _instance.AudioManager.AudioSourceUIEffects;
        GetComponent<Toggle>().isOn = !_audioSrc.mute;
    }
}
