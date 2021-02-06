﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InnerButtonAddListener : MonoBehaviour
{
    

   // [SerializeField]
    RectTransform _thisButtonRectTransform;
    [SerializeField]
    Vector2 _nextUIEndPos;

    IButtonInteractable buttonInteraction;

    private void Start()
    {
        //this.GetComponent<Button>().onClick.AddListener(delegate { OpenUI(); });
       // if (_thisButtonRectTransform == null)
            _thisButtonRectTransform = this.gameObject.GetComponent<RectTransform>();
    }
    public void MoveToScreen(RectTransform thisButtonRectTransform)
    {
        if (_thisButtonRectTransform == null)
            _thisButtonRectTransform = thisButtonRectTransform;

        InnerButtonBehaviour InnerButtonMove = new InnerButtonBehaviour(_thisButtonRectTransform, _nextUIEndPos);
        //buttonInteraction = InnerButtonMove;
        InnerButtonMove.ButtonBehaviour();
        Debug.Log("MoveToScreen function done");
       
    }        
}
