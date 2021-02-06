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
    List<float>_buttonDuration;

    List<Vector2> _buttonEndPos;
    List<GameObject> _children;//= new List<InnerButtonAddListener>();
   // create a list that holds the children of the currentUI (play button, controls etc)
   [SerializeField]
  

    public UINextPanelBehaviour(RectTransform currentUI, RectTransform nextUI, Vector2 currentUIEndPos, Vector2 nextUIEndPos, float duration)
    {
        this._children = new List<GameObject>();
        _currentUI = currentUI;
        _nextUI = nextUI;
        _currentUIEndPos = currentUIEndPos;
        _nextUIEndPos = nextUIEndPos;
        _duration = duration;

        //gets the children with RectTransform



        for (int i = 0; i < _nextUI.gameObject.transform.childCount; i++)
        {
            //Debug.Log("Next UI is: " + _nextUI.name);
            if (_nextUI.gameObject.transform.GetChild(i).GetComponentInChildren<InnerButtonAddListener>() == null)
                return;

            _children.Add(_nextUI.gameObject.transform.GetChild(i).gameObject);
            //   Debug.Log("Children are: " + _children[i].name);

        }


    }

   

    
    
    public void ButtonBehaviour()
    {
        
        _currentUI.DOAnchorPos(_currentUIEndPos, _duration);

        _nextUI.DOAnchorPos(_nextUIEndPos, _duration);//.SetDelay(0.15f);
        foreach (var button in this._children)
        {
            Debug.Log("Children Moe to screen: " + button.name);
        }
            //List<InnerButtonAddListener> children = new List<InnerButtonAddListener>();
            // children.AddRange(_children);

            // for (int i = 0; i < this._children.Count-1; i++)
            //  {
            //    this._children[i].MoveToScreen(this._children[i].gameObject.GetComponent<RectTransform>());
            //    Debug.Log("Children Moe to screen: "+ this._children[i].name);
            // }
            //foreach children trigger the inner button behaviour

            foreach (var button in this._children)
            {
                button.GetComponent<InnerButtonAddListener>().MoveToScreen(button.GetComponent<RectTransform>());
                 //button.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,0), 0.50f);
            }

            //_children.ForEach(_childr =>Debug.Log("Names of things in list: "+_childr.name));
            // _children.ForEach(_childr=>_childr.MoveToScreen());


    }


}
