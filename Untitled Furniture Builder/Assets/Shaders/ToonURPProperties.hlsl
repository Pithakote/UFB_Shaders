#ifndef URPTOON_UNLIT_INPUT_INCLUDED
#define URPTOON_UNLIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"

CBUFFER_START(UnityPerMaterial)
float4 _BaseMap_ST;
half4 _BaseColor;
half _Cutoff;
half _Glossiness;
half _Metallic;
float _Brightness;
float _Strength;
float _ScaleAndNumberOfRings;
CBUFFER_END

#endif
