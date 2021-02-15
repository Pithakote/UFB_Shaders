using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    private void Awake()
    {
        transform.DOPunchScale(new Vector3(2, 2, 2), 1f);
    }
}
