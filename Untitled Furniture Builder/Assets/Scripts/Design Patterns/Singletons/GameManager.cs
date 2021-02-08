using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public IButtonInteractable _buttonBehaviour;
    CommandProcessor _commandProcessor;
    audioManager _audioManager; 

    public audioManager AudioManager { get { return _audioManager; } }
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
        _commandProcessor = GetComponent<CommandProcessor>();
        _audioManager = GetComponent<audioManager>();
    }

    public void PerformButtonBehaviour(ICommand _buttonbehaviour)//called on the buttons to make the behaviours flexible for all kinds of behaviour
    {
        _commandProcessor.ExecuteBehaviour(_buttonbehaviour);
    
    }

    public void PerformUndoBehaviour()
    {
        _commandProcessor.UndoBehaviour();
    }


}

