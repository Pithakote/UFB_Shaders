using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxAnimTest : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 yMin;
    Vector3 yMax;
    void Start()
    {
        yMin = new Vector3(transform.position.x, -0.65f, transform.position.z);
        yMax = new Vector3(transform.position.x, 0f, transform.position.z);
        transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 90f), 2.0f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();      
    }

    // Update is called once per frame
    void Update()
    {      
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * 1) * 0.3f - 0.2f, transform.position.z);      
    }
}
