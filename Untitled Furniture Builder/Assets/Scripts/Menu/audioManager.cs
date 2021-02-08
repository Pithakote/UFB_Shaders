using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
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
    AudioClip _audio1, _audio2, _audio3;
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayHoverAudio()
    {
        _audioSource.clip = _audio1;
        _audioSource.PlayOneShot(_audioSource.clip);
       // if(_audioSource.isPlaying)
          //  _audioSource.Stop();
    }
    public void PlayClickAudio()
    {
        _audioSource.clip = _audio2;
        _audioSource.Play();
    }

    public void StopHoverAudio()
    {
        //_audioSource.clip = _audio1;
       // if (_audioSource.isPlaying)
           // _audioSource.Stop();
    }
}
