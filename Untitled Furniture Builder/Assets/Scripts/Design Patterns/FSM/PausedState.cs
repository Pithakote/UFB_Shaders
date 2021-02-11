using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : BaseState
{
    GameObject _pauseMenu;
    public PausedState(GameManager gameManager,
                       GameObject pauseMenu) : base(gameManager)
    {
        _pauseMenu = pauseMenu;
    }

    public override void DoStateAction()//going into the game
    {
       

        _gameManager.StateManager.CurrentState = _gameManager.StateManager.InGameState;//change to in-game state

        base.DoStateAction();
        _pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

  
}
