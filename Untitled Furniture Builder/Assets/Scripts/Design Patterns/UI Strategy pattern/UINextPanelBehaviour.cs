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
    List<float>_buttonDuration;

    List<Vector2> _buttonEndPos;

    // create a list that holds the children of the currentUI (play button, controls etc)
    List<RectTransform> _children;

    public UINextPanelBehaviour(RectTransform currentUI, RectTransform nextUI, Vector2 currentUIEndPos, Vector2 nextUIEndPos, float duration, List<RectTransform> children, List<Vector2> buttonEndPos, List<float> buttonDuration)
    {
        _currentUI = currentUI;
        _nextUI = nextUI;
        _currentUIEndPos = currentUIEndPos;
        _nextUIEndPos = nextUIEndPos;
        _duration = duration;
        _children = children;
        _buttonEndPos = buttonEndPos;
        _buttonDuration = buttonDuration;

        for(int i = 0; i < _children.Count; i++)
        {
            _children[i] = _currentUI.GetComponentInChildren<RectTransform>();
        }
        
       

    }

   

    
    
    public void ButtonBehaviour()
    {
        _currentUI.DOAnchorPos(_currentUIEndPos, _duration);      

        _nextUI.DOAnchorPos(_nextUIEndPos, _duration).SetDelay(0.15f);    
        
       

        for (int i = 0; i < _children.Count; i++)
        {
            _children[i].DOAnchorPos(_buttonEndPos[i], _buttonDuration[i]);

        }


    }


}
