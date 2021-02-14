using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(ButtonListenerManager))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(SaveTest))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public IButtonInteractable _buttonBehaviour;
   
    AudioManager _audioManager;
    ButtonListenerManager _buttonListenerManager;
    StateManager _stateManager;
    SaveTest _saveTest;

    public SaveTest SaveTest { get { return _saveTest; } }

    public AudioManager AudioManager { get { return _audioManager; } }
    public StateManager StateManager { get { return _stateManager; } }

    #region States Declaration Region
    
  

    #endregion
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
        _stateManager = GetComponent<StateManager>();
        _saveTest = GetComponent<SaveTest>();

    }

  


}

