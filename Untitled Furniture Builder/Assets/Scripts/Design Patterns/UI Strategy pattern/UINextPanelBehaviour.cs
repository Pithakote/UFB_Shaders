using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UINextPanelBehaviour : IButtonInteractable
{
    RectTransform _currentUI;
    RectTransform _nextUI;
    Vector2 _currentUIEndPos;
    Vector2 _nextUIEndPos;
    float _duration;
    // create a list that holds the children of the currentUI (play button, controls etc)
    
    public UINextPanelBehaviour(RectTransform currentUI, RectTransform nextUI, Vector2 currentUIEndPos, Vector2 nextUIEndPos, float duration)
    {
        _currentUI = currentUI;
        _nextUI = nextUI;
        _currentUIEndPos = currentUIEndPos;
        _nextUIEndPos = nextUIEndPos;
        _duration = duration;
    }

    
    
    public void ButtonBehaviour()
    {
        _currentUI.DOAnchorPos(_currentUIEndPos, _duration);      

        _nextUI.DOAnchorPos(_nextUIEndPos, _duration).SetDelay(0.15f);
       
    }

    
}
