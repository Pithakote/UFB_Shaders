using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : BaseState
{
    GameObject _pauseMenu;
    public InGameState(GameManager gameManager,
                       GameObject pauseMenu) : base(gameManager)
    {
        _pauseMenu = pauseMenu;

    }
    public override void DoStateAction()//going into the pause menu
    {
        

      

        _gameManager.StateManager.PausedState = new PausedState(_gameManager, _gameManager.StateManager.PauseMenuObject);
        _gameManager.StateManager.CurrentState = _gameManager.StateManager.PausedState;//change to paused state

        base.DoStateAction();
        _pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
 
    
}
