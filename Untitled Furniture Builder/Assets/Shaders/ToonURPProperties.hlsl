#ifndef URPTOON_UNLIT_INPUT_INCLUDED
#define URPTOON_UNLIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"

CBUFFER_START(UnityPerMaterial)
float4 _BaseMap_ST;
half4 _AmbientColor;
half _Cutoff;
float _Specular;
//half _Glossiness;
half _Metallic;
float _Brightness;
float _Strength;
float _Diffuse;

float _Smoothness;
float _Occlusion;
float _Emission;
float _ScaleAndNumberOfRings;
float _Alpha;
CBUFFER_END

#endif
