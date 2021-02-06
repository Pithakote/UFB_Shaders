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
    [SerializeField]
    List<InnerButtonAddListener> _children = new List<InnerButtonAddListener>();

    public UINextPanelBehaviour(RectTransform currentUI, RectTransform nextUI, Vector2 currentUIEndPos, Vector2 nextUIEndPos, float duration)
    {
        _currentUI = currentUI;
        _nextUI = nextUI;
        _currentUIEndPos = currentUIEndPos;
        _nextUIEndPos = nextUIEndPos;
        _duration = duration;

        //gets the children with RectTransform


        



    }

   

    
    
    public void ButtonBehaviour()
    {
        _currentUI.DOAnchorPos(_currentUIEndPos, _duration);      

        _nextUI.DOAnchorPos(_nextUIEndPos, _duration).SetDelay(0.15f);

        for (int i = 0; i < _nextUI.gameObject.transform.childCount; i++)
        {
            Debug.Log("Next UI is: " + _nextUI.name);
            _children.Add(_nextUI.gameObject.transform.GetChild(i).GetComponentInChildren<InnerButtonAddListener>());
            _children[i].MoveToScreen();
        }
        //foreach children trigger the inner button behaviour
        /*
        for (int i = 0; i < _children.Count; i++)
        {
             _children[i].MoveToScreen();
            //_children[i].DOAnchorPos(_buttonEndPos[i], _buttonDuration[i]);
            
        }
        */

    }


}
