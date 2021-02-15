using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMainMenuManager : LocalManager
{
    [SerializeField] RectTransform cog1, cog2;
    protected override void SetInitialState()
    {

        _instance.StateManager.CurrentState = _instance.StateManager.MainMenuState;

      
    }

    

   




}
