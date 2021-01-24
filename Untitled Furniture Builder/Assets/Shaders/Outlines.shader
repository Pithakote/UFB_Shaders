Shader "Custom/Outlines"
{
    Properties
    {
        _Thickness("Thiskness", Float) = 1
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" "RednerPipeline" = "UniversalPipeline"}
        LOD 100

        Pass
        {
         Name "Outlines"
         Cull Front
         ZWrite On
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
