using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocalManager: MonoBehaviour
{
    protected GameManager _instance;
    protected abstract void SetInitialState();
    [SerializeField] protected GameObject _radioObject, _radioInstantiation;
    protected SaveObject so;

    protected bool _radioActive;
    [SerializeField] protected Transform _radioLocation;
    void ChangeAudioSource()
    {
        /*
        if (_instance.AudioManager.AudioSourceBackgroundMusic == null)
            return;

            if (_radioObject.GetComponent<AudioSource>().isPlaying)
            _radioObject.GetComponent<AudioSource>().Stop();

        
            _radioObject.GetComponent<AudioSource>().clip = _instance.AudioManager.AudioSourceBackgroundMusic.clip;
            _radioObject.GetComponent<AudioSource>().time = _instance.AudioManager.AudioSourceBackgroundMusic.time;
        
        if(_instance.AudioManager.AudioSourceBackgroundMusic.isPlaying)
        _instance.AudioManager.AudioSourceBackgroundMusic.Stop();

        //if (!_radioObject.GetComponent<AudioSource>().isPlaying)
        //    _radioObject.GetComponent<AudioSource>().Play();

      //  if(_instance.AudioManager.AudioSourceBackgroundMusic != _radioObject.GetComponent<AudioSource>())
          _instance.AudioManager.AudioSourceBackgroundMusic = _radioObject.GetComponent<AudioSource>();
        */

       // _instance.AudioManager.AudioSourceBackgroundMusic.Pause();
        _instance.AudioManager.RadioObject.transform.position = _radioLocation.position;
        _instance.AudioManager.RadioObject.transform.localScale = _radioLocation.transform.localScale;
        _instance.AudioManager.RadioObject.transform.localRotation = _radioLocation.transform.localRotation;


        _instance.AudioManager.AudioSourceBackgroundMusic.spatialBlend = 1; //for 3D
       // _instance.AudioManager.RadioObject.GetComponent<AudioSource>().clip = _instance.AudioManager.AudioSourceBackgroundMusic.clip;
       // _instance.AudioManager.RadioObject.GetComponent<AudioSource>().time = _instance.AudioManager.AudioSourceBackgroundMusic.time;

       // if (_instance.AudioManager.RadioObject != null)
          //  _instance.AudioManager.AudioSourceBackgroundMusic = _instance.AudioManager.RadioObject.GetComponent<AudioSource>();

       // _instance.AudioManager.AudioSourceBackgroundMusic.Play();
    }


    void SpawnRadio()
    {
        so = SaveManager.Load();

        //  if(_radioObject == null)
        //     _radioObject = Instantiate(_radioObject, new Vector3(0.226f, 2.154f, 32.189f), Quaternion.identity).gameObject;

        
       // if (_radioObject == null)
       //     return;

        if (so.canSpawnRadio)
        {
            //Instantiate(_radioObject, new Vector3(0.226f, 2.154f, 32.189f), Quaternion.identity);
           // _radioActive = true;
            ChangeAudioSource();
        }
       // else
            //_radioActive = false;

        //_radioObject.SetActive(_radioActive);
        

        /*
        if (_instance.AudioManager.RadioObject == null)
        {
            _radioObject = Instantiate(_radioInstantiation, Vector3.zero, Quaternion.identity).gameObject;
            _instance.AudioManager.RadioObject = _radioObject;
        }
        else
            _radioObject = _instance.AudioManager.RadioObject;

        _radioObject.transform.position = _radioLocation.position;
        _radioObject.transform.forward = _radioLocation.forward;
        */


    }

    protected virtual void Awake()
    {
        _instance = GameManager.Instance;

        SetInitialState();
        SpawnRadio();
    }
    protected virtual void Start()
    {
        
    }
   

}
