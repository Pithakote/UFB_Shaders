using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class UINextPanelBehaviour : IButtonInteractable
{
    RectTransform _currentUI;
    RectTransform _nextUI;
    Vector2 _currentUIEndPos;
    Vector2 _nextUIEndPos;
    float _duration;
    
    List<GameObject> _children;  

    public UINextPanelBehaviour(RectTransform currentUI,
                                RectTransform nextUI,
                                Vector2 currentUIEndPos,
                                Vector2 nextUIEndPos,
                                float duration
                               )
    {
        this._children = new List<GameObject>();
        _currentUI = currentUI;
        _nextUI = nextUI;
        _currentUIEndPos = currentUIEndPos;
        _nextUIEndPos = nextUIEndPos;
        _duration = duration;
        


        for (int i = 0; i < _nextUI.gameObject.transform.childCount; i++)
        {
            //if the InnerButtonAddListener is not present continue the loop but don't add
            if (_nextUI.gameObject.transform.GetChild(i).GetComponentInChildren<InnerButtonAddListener>() == null)
                continue;

          //  if (_nextUI.gameObject.transform.GetChild(i).GetComponentInChildren<InnerButtonAddListener>() != null)
                _children.Add(_nextUI.gameObject.transform.GetChild(i).gameObject);               

        }        

    }    
    
    public void ButtonBehaviour()
    {
        if(_currentUI != null)
        _currentUI.DOAnchorPos(_currentUIEndPos, _duration);

        _nextUI.DOAnchorPos(_nextUIEndPos, _duration);

        //all children's behaviour

        
                  
        _children.ForEach(childr => childr.GetComponent<InnerButtonAddListener>().MoveToScreen());
        

    }


}
