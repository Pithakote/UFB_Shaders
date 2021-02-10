using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonQuitListener : ButtonAddListener
{
    public void QuitGame()
    {
        Debug.Log("Quitting game..");
        Application.Quit();
    }
    protected override void ButtonAction()
    {
        QuitGame();
    }
    public override ICommand ReturnButtonBehaviour()
    {
        throw new System.NotImplementedException();
    }

   
}
