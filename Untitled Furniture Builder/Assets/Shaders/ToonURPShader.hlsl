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
    
    float3 positionOS : POSITION;//position in world space
    float3 normalOS : NORMAL;   
    float4 tangentOS : TANGENT;
    float2 uv : TEXCOORD0;
   
    UNITY_VERTEX_INPUT_INSTANCE_ID

};

struct Varyings
{
    
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


   output.positionCS = CalculatePositionCSWithShadowCasterLogic(vertexInput.positionWS, normalInput.normalWS);

    output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
    return output;
}

half4 Fragment(Varyings input) : SV_Target
{
    #ifdef SHADOW_CASTER_PASS

            return 0;
    #else
    half2 uv = input.uv;
    half4 texColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
    half3 color = texColor.rgb * _TextureColor.rgb;
    half alpha = texColor.a * _TextureColor.a;
   
    // Initialize some information for the lighting function
    InputData lightingInput = (InputData)0;
   lightingInput.positionWS = input.positionWS;
   // lightingInput.normalWS =  NormalizeNormalPerPixel(input.normalWS); // No need to renormalize, since triangles all share normals
    lightingInput.normalWS =  (input.normalWS); // No need to renormalize, since triangles all share normals
    lightingInput.viewDirectionWS = GetViewDirectionFromPosition(input.positionWS);
    //lightingInput.positionCS = CalculatePositionCSWithShadowCasterLogic(input.positionCS, input.normalWS);
    lightingInput.shadowCoord = CalculateShadowCoord(input.positionWS, input.positionCS);

    float3 albedo = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv).rgb 
                    * half3(unity_SHAr.w, unity_SHAg.w, unity_SHAb.w)
                    * _TextureColor.rgb;
   
    albedo *= Toon(input.normalWS, _MainLightPosition.xyz) * _Strength + _Brightness;
  
    half4 fragColor = UFBUniversalFragmentPBR(lightingInput, albedo, _Metallic, _Specular, _Smoothness, _Occlusion, _Emission* _TextureColor, _Alpha);// *half4(color, 1);
   
    return fragColor ;
#endif
}
#endif