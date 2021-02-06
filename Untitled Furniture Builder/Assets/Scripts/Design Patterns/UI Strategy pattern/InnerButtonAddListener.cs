using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InnerButtonAddListener : MonoBehaviour
{
    [SerializeField]
    RectTransform _currentUI;
    [SerializeField]
    RectTransform _nextUI;
    [SerializeField]
    Vector2 _currentUIEndPos;
    
    [SerializeField]
    float _duration;

    [SerializeField]
    RectTransform _thisButtonRectTransform;
    [SerializeField]
    Vector2 _nextUIEndPos;

    IButtonInteractable buttonInteraction;

    private void Awake()
    {
        //this.GetComponent<Button>().onClick.AddListener(delegate { OpenUI(); });
        if(_thisButtonRectTransform == null)
        _thisButtonRectTransform = gameObject.GetComponent<RectTransform>();
    }
    public void MoveToScreen()
    {
        InnerButtonBehaviour InnerButtonMove = new InnerButtonBehaviour(_thisButtonRectTransform, _nextUIEndPos);
        buttonInteraction = InnerButtonMove;
        buttonInteraction.ButtonBehaviour();
       
    }        
}
