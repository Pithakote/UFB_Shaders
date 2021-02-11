using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InnerButtonAddListener : MonoBehaviour
{
    

   // [SerializeField]
    RectTransform _thisButtonRectTransform;
    [SerializeField]
    Vector2 _ComeToScreenPosition, _HideFromScreenPosition;
    [SerializeField]
    float duration;
    [SerializeField]
    bool _IsBackButton;

    private void Start()
    {
        //this.GetComponent<Button>().onClick.AddListener(delegate { OpenUI(); });
       if (_thisButtonRectTransform == null)
            _thisButtonRectTransform = this.gameObject.GetComponent<RectTransform>();

        if(_IsBackButton)
        this.GetComponent<Button>().onClick.AddListener(delegate { MoveAwayFromScreen(); });
    }
    public void MoveToScreen()
    {     

        InnerButtonBehaviour InnerButtonMove = new InnerButtonBehaviour(_thisButtonRectTransform, _ComeToScreenPosition, duration);
        //buttonInteraction = InnerButtonMove;
        InnerButtonMove.ButtonBehaviour();
        Debug.Log("MoveToScreen function done");

    }

    public void MoveAwayFromScreen()
    {

        InnerButtonBehaviour InnerButtonMoveAway = new InnerButtonBehaviour(_thisButtonRectTransform, _HideFromScreenPosition, duration);
        //buttonInteraction = InnerButtonMove;
        InnerButtonMoveAway.ButtonBehaviour();
      //  Debug.Log("MoveToScreen function done");

    }
}
