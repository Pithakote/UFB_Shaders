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
    float duration, _delayTime;
    [SerializeField]
    bool _IsBackButton;
    

    private void Start()
    {
                
        /*
        if(_IsBackButton)
        this.GetComponent<Button>().onClick.AddListener(delegate { MoveAwayFromScreen(); });
        */
    }
    public void MoveToScreen()
    {
        if (_thisButtonRectTransform == null)
            _thisButtonRectTransform = this.gameObject.GetComponent<RectTransform>();

        InnerButtonBehaviour InnerButtonMove = new InnerButtonBehaviour(_thisButtonRectTransform,
                                                                        _ComeToScreenPosition,
                                                                        duration,
                                                                        _delayTime);
        //buttonInteraction = InnerButtonMove;
        InnerButtonMove.ButtonBehaviour();
        //  Debug.Log("MoveToScreen function done");
        Debug.Log("Initial UI moving for "+gameObject.name);

    }
    /*
    public void MoveAwayFromScreen()
    {

        InnerButtonBehaviour InnerButtonMoveAway = new InnerButtonBehaviour(_thisButtonRectTransform,
                                                                            _HideFromScreenPosition,
                                                                            duration,
                                                                            0);
        //buttonInteraction = InnerButtonMove;
        InnerButtonMoveAway.ButtonBehaviour();
      //  Debug.Log("MoveToScreen function done");

    }
    */
}
