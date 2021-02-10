using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResumePauseButtonListener : ButtonAddListener
{
    public override ICommand ReturnButtonBehaviour()
    {
        throw new System.NotImplementedException();
    }

    protected override void ButtonAction()
    {
        //_pauseMenu.SetActive(false);
       // Cursor.visible = false;
       // Time.timeScale = 1f;

        //_gameManager.StateManager.CurrentState = _gameManager.StateManager.InGameState;//change to in-game state
        _instance.StateManager.CurrentState = _instance.StateManager.PausedState;
        _instance.ButtonListenerManager.PerformButtonBehaviour(_instance.StateManager.CurrentState);
    }
}
