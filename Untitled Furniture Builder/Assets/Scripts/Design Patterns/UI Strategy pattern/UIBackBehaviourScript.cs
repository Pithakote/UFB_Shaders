using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackBehaviourScript : ButtonAddListener, ICommand
{
    
    public override ICommand ReturnButtonBehaviour()
    {
        return this;
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }

  

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    protected override void ButtonAction()
    {
       // Debug.Log("Undo Button Class");
        _instance.ButtonListenerManager.PerformUndoBehaviour();
    }
}
