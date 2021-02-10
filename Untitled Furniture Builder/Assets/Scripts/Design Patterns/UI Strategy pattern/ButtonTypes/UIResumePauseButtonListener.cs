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
        _instance.StateManager.CurrentState = _instance.StateManager.InGameState;
     
    }
}
