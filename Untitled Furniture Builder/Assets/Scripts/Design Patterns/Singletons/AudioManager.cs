using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    [Header("SFX Clips")]
    [SerializeField]
    AudioClip _audio1;
    [SerializeField]
    AudioClip _audio2, _audio3,  _backgroundMusic;

    //[Header("BG Music Clips")]
    //[SerializeField]  AudioClip[] bgMusic;

    [SerializeField]
    AudioSource _audioSourceUIEffects, _audioSourceBackgroundMusic;
    [SerializeField]
    GameObject _radioObject, _bgAudioSource;
    public AudioSource AudioSourceBackgroundMusic { get { return _audioSourceBackgroundMusic; } set { _audioSourceBackgroundMusic = value; } }
    public AudioSource AudioSourceUIEffects{ get { return _audioSourceUIEffects; } }
    public GameObject RadioObject { get { return _radioObject; } set { _radioObject = value; } }
    public GameObject BGAudioSource { get { return _bgAudioSource; } }

    [Header("BG Music Clips")]
    [SerializeField]
    List<AudioClip> _backupClip, _bgMusic;

    private void Awake()
    {
        // _audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
       
    }
    private void Start()//only triggers once even for a singleton only onenable and awake are triggered everytime scene is changed
    {
        if (_audioSourceBackgroundMusic.spatialBlend != 0)
            _audioSourceBackgroundMusic.spatialBlend = 0;//for 2D
        PlayBGMusic();
    }

    private void Update()
    {
        if (!AudioSourceBackgroundMusic.isPlaying)
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
    void SelectMusicClip()
    {
        if (_bgMusic.Count <= 0)
        {
            _bgMusic.AddRange(_backupClip);
            _backupClip.Clear();
        }
        // if (_audioSourceBackgroundMusic.clip == null)
        int _clipIndex = Random.Range(0, _bgMusic.Count-1);
        _backupClip.Add(_bgMusic[_clipIndex]);
        

        _audioSourceBackgroundMusic.clip = _bgMusic[_clipIndex];

        _bgMusic.RemoveAt(_clipIndex);
    }
    public void PlayBGMusic()
    {
        BackgroundMusicSourceNullCheck();

        SelectMusicClip();

       

        _audioSourceBackgroundMusic.Play();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }
}
