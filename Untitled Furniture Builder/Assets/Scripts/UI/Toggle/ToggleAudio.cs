using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleAudio : MonoBehaviour
{
    [SerializeField]
    protected AudioSource _audioSrc;

    protected GameManager _instance;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
       
    }
    protected virtual void Start()
    {
        _instance = GameManager.Instance;

    }

    public void ToggleAudioType(bool isMute)
    {
        _audioSrc.mute = !isMute;
    }
}
