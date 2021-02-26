using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineChanger : MonoBehaviour
{
    Renderer _renderer;
    
    Color _newColor;
    bool _changeColor;
    
    float _timer;
    [SerializeField]
    float _changeTime,_outlineChangeSpeed, _outlineMinumumScale,_outlineMaxScale, _outlineScale;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _timer = 0.0f;
       // _changeTime = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
         _timer += Time.deltaTime;
        //_timer = Mathf.Sin(Time.time* _frequency) * _amplitude;
        if (_timer >= _changeTime)//change the float value here to change how long it takes to switch.
        {
            _newColor = new Color(Random.value, Random.value, 1.0f);
            if (_renderer.material.HasProperty("_OutlineColor") == true)
                _renderer.material.SetColor("_OutlineColor", (_newColor));
            _timer = 0;
        }
        _outlineScale = Mathf.PingPong(Time.time* _outlineChangeSpeed, _outlineMaxScale - _outlineMinumumScale) + _outlineMinumumScale;
        _renderer.material.SetFloat("_OutlineXScale", _outlineScale);
        _renderer.material.SetFloat("_OutlineYScale", _outlineScale);
        _renderer.material.SetFloat("_OutlineZScale", _outlineScale);



    }
}
