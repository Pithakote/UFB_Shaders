using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UICogBehaviour : IButtonInteractable
{
    RectTransform _cog1;
    RectTransform _cog2;
    Vector3 _rotationVector;
    float _duration;
    public UICogBehaviour(RectTransform cog1, RectTransform cog2, Vector3 rotationVector, float duration) 
    {
        _cog1 = cog1;
        _cog2 = cog2;
        _rotationVector = rotationVector;
        _duration = duration;
    }

    public void ButtonBehaviour()
    {
        _cog1.DOLocalRotate(-_rotationVector, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
        _cog2.DOLocalRotate(_rotationVector, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
    }
}
