using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InnerButtonBehaviour : IButtonInteractable
{
    
    Vector2 _comeToScreenPosition, _awarFromScreenPosition;
    RectTransform _thisButtonRectTransform;
    float _duration;
    
    
    public InnerButtonBehaviour(RectTransform thisButtonRectTransform, Vector2 comeToScreenPosition, float duration)
    {
        _thisButtonRectTransform = thisButtonRectTransform;
        _comeToScreenPosition = comeToScreenPosition;
        _duration = duration;
    }
    public void ButtonBehaviour()
    {
        _thisButtonRectTransform.DOAnchorPos(_comeToScreenPosition, _duration);
    }

  
}
