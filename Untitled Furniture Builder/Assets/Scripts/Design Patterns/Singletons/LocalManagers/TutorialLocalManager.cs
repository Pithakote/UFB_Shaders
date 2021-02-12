using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UITweenManager))]
public class TutorialLocalManager : LevelManager
{
	protected override void SetInitialState()
	{
		base.SetInitialState();
		//base class abstraction
		//_instance.StateManager.PauseMenuObject = _pauseMenuObject;
		//_instance.StateManager.InGameState = new InGameState(_instance, _pauseMenuObject);
		//_instance.StateManager.CurrentState = _instance.StateManager.InGameState;
		//
		//Debug.Log(_instance.StateManager.CurrentState + " is being called");

	}
}
