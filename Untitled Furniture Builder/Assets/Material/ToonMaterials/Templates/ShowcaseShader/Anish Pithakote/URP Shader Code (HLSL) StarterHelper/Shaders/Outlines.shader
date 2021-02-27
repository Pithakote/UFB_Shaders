Shader "Custom/Outlines"
{
    Properties
    {
        _Thickness("Thiskness", Float) = 1
        _Color("Color", Color) = (1,1,1,1)
        _Alpha("Alpha", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" "RednerPipeline" = "UniversalPipeline"}
        LOD 100

        Pass
        {
         Name "Outlines"
         Cull Front
         ZWrite Off
         Blend One OneMinusSrcAlpha
         //cull front faces
       //Cull Back

        HLSLPROGRAM
#pragma prefer_hlslcc gles
  #pragma exclude_renderers d3d11_9x

#pragma vertex Vertex
#pragma fragment Fragment

#include "Outlines.hlsl"
        ENDHLSL
        }
    }
}
