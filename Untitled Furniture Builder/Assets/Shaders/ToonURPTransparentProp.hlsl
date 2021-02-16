#ifndef URPTOON_UNLIT_INPUT_INCLUDED
#define URPTOON_UNLIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"

CBUFFER_START(UnityPerMaterial)
float4 _BaseMap_ST;
half4 _TextureColor;
float4 _OuterRimColor;
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

float _LightSpecCutOff;
float _LightSpecCutOffSmoothness;
float _ViewSpecCutOff;
half4 _SpecularColor;
float _RimPower;
float _FresnelInnerRimPower;
float _FresnelOuterRimSmoothness;
float _FresnelInnerRimSmoothness;
half4 _FresnelOuterRimColor;
half4 _InnerRimColor;
half4 _FinalColor;
CBUFFER_END

#endif
