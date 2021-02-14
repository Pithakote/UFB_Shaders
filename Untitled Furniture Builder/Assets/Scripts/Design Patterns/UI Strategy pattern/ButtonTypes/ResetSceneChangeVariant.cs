using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSceneChangeVariant : SceneChangeButtonListener
{
    public override void Execute()
    {
        _instance.SaveTest.ResetProgress();
        _instance.AudioManager.AudioSourceBackgroundMusic.spatialBlend = 0;
       // _instance.AudioManager.AudioSourceBackgroundMusic.Stop();
     //   _instance.AudioManager.AudioSourceBackgroundMusic = _instance.AudioManager.BGAudioSource.GetComponent<AudioSource>();
     //   _instance.AudioManager.AudioSourceBackgroundMusic.Play();
        base.Execute();
    }


}
