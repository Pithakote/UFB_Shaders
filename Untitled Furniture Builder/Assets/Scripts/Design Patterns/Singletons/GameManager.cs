using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(ButtonListenerManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public IButtonInteractable _buttonBehaviour;
   
    AudioManager _audioManager;
    ButtonListenerManager _buttonListenerManager;

    public AudioManager AudioManager { get { return _audioManager; } }
    public ButtonListenerManager ButtonListenerManager { get { return _buttonListenerManager; } }
    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(this.gameObject);
    }
    private void Awake()
    {
        
        _audioManager = GetComponent<AudioManager>();
        _buttonListenerManager = GetComponent<ButtonListenerManager>();
    }

  


}

