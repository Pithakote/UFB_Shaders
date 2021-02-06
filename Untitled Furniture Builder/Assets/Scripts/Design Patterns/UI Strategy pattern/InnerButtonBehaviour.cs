using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InnerButtonBehaviour : IButtonInteractable
{
    
    Vector2 _comeToScreenPosition, _awarFromScreenPosition;
    RectTransform _thisButtonRectTransform;
    
    
    public InnerButtonBehaviour(RectTransform thisButtonRectTransform, Vector2 comeToScreenPosition)
    {
        _thisButtonRectTransform = thisButtonRectTransform;
        _comeToScreenPosition = comeToScreenPosition;
    }
    public void ButtonBehaviour()
    {
        _thisButtonRectTransform.DOAnchorPos(_comeToScreenPosition, 0.35f);
    }

  
}
