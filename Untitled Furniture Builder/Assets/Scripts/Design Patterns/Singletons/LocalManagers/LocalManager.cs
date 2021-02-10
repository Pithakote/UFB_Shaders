using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocalManager: MonoBehaviour
{
    protected GameManager _instance;
    protected abstract void SetInitialState();
    protected virtual void Start()
    {
        _instance = GameManager.Instance;
        SetInitialState();
    }
   
}
