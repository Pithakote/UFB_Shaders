using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveShowcase : MonoBehaviour
{
    Renderer _renderer;
    float _emissiveness;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.shader = Shader.Find("Custom/ToonURPShader"); //finds the shader
    }

    // Update is called once per frame
    void Update()
    {
         _emissiveness = Mathf.PingPong(Time.time,1.0f);
        _renderer.material.SetFloat("_Emission", _emissiveness);

    }
}
