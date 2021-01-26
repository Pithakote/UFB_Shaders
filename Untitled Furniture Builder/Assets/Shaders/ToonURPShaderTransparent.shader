
Shader "Custom/ToonURPShaderTransparent"
{
    Properties
    {
     // [KeywordEnum(SPECULAR_COLOR,METALLIC_COLOR)] GLOSS("Gloss mode subset", Float) = 0
       // [Toggle(_SPECULAR_SETUP)] _Gloss("Gloss?", Float) = 0
        [Space(10)] [Header(Glossiness Options)] [KeywordEnum(SPECULAR_SETUP,METALLIC)] [HideInInspector] _Gloss("Glossiness Type", Float) = 0
         
        [Space(10)][Header(Surface Options)][HideInInspector]_BaseMap("Color (RGB) Alpha (A)", 2D) = "white" {}
        [HideInInspector]_AmbientColor("AmbientColor", Color) = (1, 1, 1, 1)
             
       [Space(10)][Header(Shader Settings)][HideInInspector] _Brightness("Brightness", Range(-1,1)) = -0.47
        [HideInInspector]_Strength("Strength/Intensity", Range(0,50)) = 6.6

       [HideInInspector] _Diffuse("Diffuse", Range(0,50)) = 1
         [HideInInspector]   _Specular("Specular", Range(0,1)) = 0
       [HideInInspector] _Metallic("Metallic", Range(0,1)) = 1.06
        [HideInInspector]_Smoothness("Smoothness", Range(0,1)) = 0.421
          [HideInInspector]  _Occlusion("Occlusion", Range(0,1)) = 0.138
        [HideInInspector]_Emission("Emission", Range(0,1)) = 0
        [HideInInspector]_Alpha("Alpha", Range(0,1)) = 0.2
       [HideInInspector] _ScaleAndNumberOfRings("ScaleAndNumberOfRings", Range(0.1,1)) = 0.338

            _SpecularColor("SpecularColor", Color) = (1, 1, 1, 1)
            _FresnelOuterRimColor("FresnelOuterRimColor", Color) = (1, 1, 1, 1)
            _LightSpecCutOff("LightSpecularCutoff", Range(0,1)) = 0.5
            _LightSpecCutOffSmoothness("LightSpecularCutoffSmoothness", Range(0,1)) = 0.05
            _ViewSpecCutOff("ViewSpecCutOff", Range(0,1)) = 0.5
            _FresnelInnerRimPower("FresnelInnerRimPower", Range(0,20)) = 1
            _FresnelOuterRimSmoothness("FresnelOuterRimSmoothness", Range(0,1)) = 0.5
            
            // _AmbientLight("AmbientLight",Float) = half3(unity_SHAr.w,unity_SHAg.w,unity_SHAb.w)
            _OutlineThickness("Outline Thickness", Float) = 1.07
        _OutlineColor("Outline Color", Color) = (0,0,0,255)
    }
       // CustomEditor "ToonURPShaderGUI"
        SubShader
        {
            Tags
            {
                "RenderType" = "Transparent"
                "RenderPipeline" = "UniversalPipeline"
                 "Queue" = "Transparent"
            }
            // LOD 100
          
             Pass//outline pass
           {
            Name "Outlines"
            Cull Front
            ZWrite Off
           // ZTest Always
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
            

            Pass//main object render
            {
                  LOD 100
            ZWrite Off
                Blend One OneMinusSrcAlpha
                     Name "ForwardLit"
                     Tags { "LightMode" = "UniversalForward" }
                Lighting On
                    //Tags { "LightMode" = "SRPDefaultUnlit" }
                   // Cull Back
                  // Use same blending / depth states as Standard shader
               // Blend[_SrcBlend][_DstBlend]
                //ZWrite[_ZWrite]
                //Cull[_Cull]

                HLSLPROGRAM
                // Signal this shader requires geometry programs
                #pragma prefer_hlslcc gles
                #pragma exclude_renderers d3d11_9x
                #pragma target 2.0
               // #pragma require geometry
                // Material Keywords
               // Material Keywords
            #define _COLORED_SPECULAR
            //#define _ALPHATEST_ON
            #define _ALPHAPREMULTIPLY_ON
            //#define shader_feature _ _SPECGLOSSMAP
           // #pragma shader_feature _ _GLOSS_SPECULAR_SETUP _GLOSS_METALLIC
           #pragma multi_compile _ _GLOSS_SPECULAR_SETUP _GLOSS_METALLIC

#if _GLOSS_SPECULAR_SETUP//set from ToonURPShaderGUI
#define _SPECULAR_SETUP
#elif _GLOSS_METALLIC
#define _METALLIC

#endif
            
//#if (_Gloss==0)
//        #define _SPECULAR_SETUP
//#else
//        #define _METALLIC
//#endif
                  // #define _METALLIC
                  // #define _Gloss
                  // #pragma shader_feature _SPECULAR_SETUP
                   //#define _SPECULAR_SETUP
                  // #define _METALLIC_SETUP
            
            #define _GLOSSINESS_FROM_BASE_ALPHA
            #pragma shader_feature _NORMALMAP
            #define _EMISSION
           
             // #pragma vertex vert;
             //   #pragma fragment frag;
                // Lighting and shadow keywords
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
                //#pragma multi_compile _ _ADDITIONAL_LIGHTS

           // #define _ADDITIONAL_LIGHTS;
           // #define _ADDITIONAL_LIGHTS_VERTEX;
           // #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
              // #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
                #define _ _SHADOWS_SOFT

            //#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

               #define _ADDITIONAL_LIGHTS;
            #define _ADDITIONAL_LIGHTS_VERTEX;
            // Unity defined keywords
           // #pragma multi_compile _DIRLIGHTMAP_COMBINED
          //  #pragma multi_compile _LIGHTMAP_ON
            
            

                // Register our functions
                #pragma vertex Vertex
              //  #pragma geometry Geometry
                #pragma fragment Fragment
              

                // Include our logic file
                #include "ToonURPShaderTransparent.hlsl"    

                ENDHLSL
            }
             
             
          
        }
        CustomEditor "ToonURPShaderTransparentGUI"
}
