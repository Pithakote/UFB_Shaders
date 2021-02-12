using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    /*
    public GameObject audioObj1;
    public GameObject audioObj2;
    public GameObject audioObj3;

    public void SpawnAudio1()
    {
        Instantiate(audioObj1, transform.position, transform.rotation);
    }
    public void SpawnAudio2()
    {
        Instantiate(audioObj2, transform.position, transform.rotation);
    }
    public void SpawnAudio3()
    {
        Instantiate(audioObj3, transform.position, transform.rotation);
    }
    */

    [SerializeField]
    AudioClip _audio1, _audio2, _audio3, _backgroundMusic;
    [SerializeField]
    AudioSource _audioSourceUIEffects, _audioSourceBackgroundMusic;

    public AudioSource AudioSourceBackgroundMusic { get { return _audioSourceBackgroundMusic; } }
    public AudioSource AudioSourceUIEffects{ get { return _audioSourceUIEffects; } }

    private void Awake()
    {
       // _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        PlayBGMusic();
    }
    void UIEffectSourceNullCheck()
    {
        if (_audioSourceUIEffects == null)
            return;
    }
    
    public void PlayHoverAudio()
    {
        UIEffectSourceNullCheck();

        _audioSourceUIEffects.clip = _audio1;
        _audioSourceUIEffects.PlayOneShot(_audioSourceUIEffects.clip);
       // if(_audioSource.isPlaying)
          //  _audioSource.Stop();
    }
    public void PlayClickAudio()
    {
        UIEffectSourceNullCheck();

        _audioSourceUIEffects.clip = _audio2;
        _audioSourceUIEffects.PlayOneShot(_audioSourceUIEffects.clip);
    }

    public void StopHoverAudio()
    {
        //_audioSource.clip = _audio1;
       // if (_audioSource.isPlaying)
           // _audioSource.Stop();
    }

    void BackgroundMusicSourceNullCheck()
    {
        if (_audioSourceBackgroundMusic == null)
            return;
    }

    void PlayBGMusic()
    {
        BackgroundMusicSourceNullCheck();

        if (_audioSourceBackgroundMusic.clip == null)
            _audioSourceBackgroundMusic.clip = _backgroundMusic;

        _audioSourceBackgroundMusic.Play();
    }
}
