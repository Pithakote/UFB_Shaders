//Make sure this file is not included twice
#ifndef TOONURPSHADER_INCLUDED
#define TOONURPSHADER_INCLUDED

//Include helper functions from URP
#include "UFBLighting.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "NMGGeometryHelpers.hlsl"
//#include "Packages/com.unity.render-pipelines.universal/Shaders/UnlitInput.hlsl" //included for declaring the variables in a separate file 
#include "ToonURPProperties.hlsl" //included for declaring the variables in a separate file 

struct Attributes
{
    /*
    float4 positionOS       : POSITION;
    float2 uv               : TEXCOORD0;
    float3 positionWS :TEXCOORD1;
    float3 normalOS : NORMAL;
    float4 tangentOS :TANGENT;
    //float3 normalWS                 : TEXCOORD1;
    */
    //  UNITY_FOG_COORDS(1)
    float3 positionOS : POSITION;//position in world space
    float3 normalOS : NORMAL;   
    float4 tangentOS : TANGENT;
    float2 uv : TEXCOORD0;
   
    UNITY_VERTEX_INPUT_INSTANCE_ID

};

struct Varyings
{
    /*
    float2 uv        : TEXCOORD0;
    float fogCoord : TEXCOORD1;
    float3 positionWS :TEXCOORD2;
    float4 positionCS : SV_POSITION;
    float3 normalWS : TEXCOORD3;
    */
    //  UNITY_FOG_COORDS(1)
    float3 positionWS : TEXCOORD0;//position in world space
    float2 uv : TEXCOORD1;
    float3 normalWS : TEXCOORD2;

    float4 positionCS : SV_POSITION;//Position in clip space

    UNITY_VERTEX_INPUT_INSTANCE_ID
        UNITY_VERTEX_OUTPUT_STEREO
};


float Toon(float3 normal, float3 lightDir)
{
    float NdotL = max(0.3, dot((normal), (lightDir)));
   // return dot((normal), (lightDir));
    return floor(NdotL / _ScaleAndNumberOfRings);//goes from 0 to 0.3, 0.3 to 0.6, 0.6 to 0.9 and 0.9 to 1. So 4 partations
}
Varyings Vertex(Attributes input)
{
    Varyings output = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
    output.positionWS = vertexInput.positionWS;
    output.positionCS = vertexInput.positionCS;
   // output.positionCS = GetShadowPositionHClip(input)'

    
  //  output.fogCoord = ComputeFogFactor(vertexInput.positionCS.z);
    
    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
    output.normalWS = (normalInput.normalWS);


  //  output.positionCS = CalculatePositionCSWithShadowCasterLogic(output.positionCS, output.normalWS);

    output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
    return output;
}

half4 Fragment(Varyings input) : SV_Target
{
// #ifdef SHADOW_CASTER_PASS
//    // If in the shadow caster pass, we can just return now
//    // It's enough to signal that should will cast a shadow
//    return half4(0,0,0,0);
//#else
  //  UNITY_SETUP_INSTANCE_ID(input);
  //  UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    half2 uv = input.uv;
    half4 texColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
    half3 color = texColor.rgb * _AmbientColor.rgb;
    half alpha = texColor.a * _AmbientColor.a;
   
    // Initialize some information for the lighting function
    InputData lightingInput = (InputData)0;
   lightingInput.positionWS = input.positionWS;
   // lightingInput.normalWS =  NormalizeNormalPerPixel(input.normalWS); // No need to renormalize, since triangles all share normals
    lightingInput.normalWS =  (input.normalWS); // No need to renormalize, since triangles all share normals
    lightingInput.viewDirectionWS = GetViewDirectionFromPosition(input.positionWS);
    lightingInput.shadowCoord = CalculateShadowCoord(input.positionWS, input.positionCS);

    float3 albedo = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv).rgb 
                    * half3(unity_SHAr.w, unity_SHAg.w, unity_SHAb.w)
                    *_AmbientColor.rgb;
    //albedo *= Toon(input.normalWS, _MainLightPosition.xyz) * _Strength + _Brightness;
    albedo *=  _Strength + _Brightness;
    float toonDotProduct = dot(input.normalWS, _MainLightPosition.xyz - lightingInput.viewDirectionWS);
    float smoothValue = smoothstep(_LightSpecCutOff, _LightSpecCutOff + _LightSpecCutOffSmoothness, toonDotProduct);
    

    float specularDotProduct = dot(input.normalWS, lightingInput.viewDirectionWS);
    float specularSmoothValue = smoothstep(_ViewSpecCutOff, _ViewSpecCutOff + _LightSpecCutOffSmoothness, specularDotProduct);

    float finalColor = (specularSmoothValue + smoothValue);// *_SpecularColor;

    float fresnel = dot(lightingInput.normalWS, lightingInput.viewDirectionWS);
    fresnel = saturate(1 - fresnel);
    fresnel = pow(fresnel, _FresnelInnerRimPower);

    fresnel = smoothstep(0.5, 0.5 +_FresnelOuterRimSmoothness,fresnel);

    fresnel = _FresnelOuterRimColor * fresnel;


    //return  half4(color, alpha);
                //return  half4(albedo, 1.0);
               // return UniversalFragmentBlinnPhong(lightingInput, albedo,1,0,0,1);
    //half4 fragColor = UniversalFragmentBlinnPhong(lightingInput, albedo, _Specular, _Smoothness,  _Emission, 1);// *half4(color, 1);
   // return UniversalFragmentPBR(inputData, albedo, metallic, specular, smoothness, occlusion, emission, alpha);
    half4 fragColor = UFBUniversalFragmentPBR(lightingInput, albedo , _Metallic,
                                                _Specular, _Smoothness, _Occlusion,
                                                finalColor* _SpecularColor, _Alpha);// *half4(color, 1);
   // half4 fragColor = LightweightFragmentBlinnPhong(lightingInput, albedo, _Specular, _Smoothness, toonDotProduct, _Alpha);// *half4(color, 1);
   // fragColor = half4(albedo,1) + MainLightRealtimeShadow(TransformWorldToShadowCoord(lightingInput.positionWS));// *half4(color, 1);
   // fragColor.a = _Alpha;
   // fragColor += Toon(input.normalWS, _MainLightPosition.xyz);
    return fragColor;// +Toon(input.normalWS, _MainLightPosition.xyz);
//#endif
}
#endif