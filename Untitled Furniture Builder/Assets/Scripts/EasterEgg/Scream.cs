using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Scream : MonoBehaviour
{
    [SerializeField]
    AudioClip[] _screamingClips;
    AudioSource _audioSrc;

    private void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        _audioSrc.clip = _screamingClips[Random.Range(0, _screamingClips.Length)];
        _audioSrc.Play();
    }
}
