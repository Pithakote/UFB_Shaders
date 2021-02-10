using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameManager))]
public class StateManager : MonoBehaviour
{
    GameManager _instance;

   

    BaseState _currentState;
    PausedState _pausedState;
    InGameState _inGameState;
    MainMenuState _mainMenuState;

    public BaseState CurrentState{get{ return _currentState; }
                                  set { _currentState = value; } }

  //  public PausedState PausedState { get { return _pausedState; } }
    
    //public InGameState InGameState { get { return _inGameState; } }
    public InGameState InGameState { get; set; }
    public PausedState PausedState { get; set; }
    public MainMenuState MainMenuState { get { return _mainMenuState; } }

    public GameObject PauseMenuObject { get; set; }

    void StateAssignment()
    {
        _mainMenuState = new MainMenuState(_instance);
        /*
        _inGameState = new InGameState(_instance, PauseMenuObject);
        _pausedState = new PausedState(_instance, PauseMenuObject);
        */
       
      

    }

    //awake because the local managers set the value of current state
    //in their start which is after awake is done in Unity's game loop
    void Awake() 
    {

        _instance = gameObject.GetComponent<GameManager>();
        StateAssignment();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _currentState.DoEscapeKeyAction();
        
    }
}
