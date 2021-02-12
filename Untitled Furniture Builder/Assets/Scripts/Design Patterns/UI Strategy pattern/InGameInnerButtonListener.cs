using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameInnerButtonListener : MonoBehaviour
{


    // [SerializeField]
    RectTransform _thisButtonRectTransform;
    [SerializeField]
    bool _IsBackButton;

    [Header("Come To Screen")]
    [SerializeField]
    Vector2 _ComeToScreenPosition;

    [SerializeField]
    float ctsDuration, _ctsDelay;

    [Header("Hide From Screen")]
    [SerializeField]
    Vector2 _HideFromScreenPosition;

    [SerializeField]
    float hfsDuration, _hfsDelay;






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
                                                                        ctsDuration,
                                                                        _ctsDelay);
        //buttonInteraction = InnerButtonMove;
        InnerButtonMove.ButtonBehaviour();
        //  Debug.Log("MoveToScreen function done");
        Debug.Log("Initial UI moving for " + gameObject.name);

    }

    public void MoveAwayFromScreen()
    {
        if (_thisButtonRectTransform == null)
            _thisButtonRectTransform = this.gameObject.GetComponent<RectTransform>();
        InnerButtonBehaviour InnerButtonMoveAway = new InnerButtonBehaviour(_thisButtonRectTransform,
                                                                            _HideFromScreenPosition,
                                                                            hfsDuration,
                                                                            _hfsDelay);
        //buttonInteraction = InnerButtonMove;
        InnerButtonMoveAway.ButtonBehaviour();
        //  Debug.Log("MoveToScreen function done");

    }

}
