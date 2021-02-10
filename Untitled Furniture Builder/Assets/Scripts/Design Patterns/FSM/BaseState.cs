using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected GameManager _gameManager;
    public BaseState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public virtual void DoStateAction()
    {
        Debug.Log(this.GetType().Name + " is being called");
    }
}
