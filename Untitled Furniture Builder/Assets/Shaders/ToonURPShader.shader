
Shader "Custom/ToonURPShader"
{
        Properties
        {
     
            [Space(10)] [Header(Glossiness Options)] [KeywordEnum(SPECULAR_SETUP,METALLIC)] [HideInInspector] _Gloss("Glossiness Type", Float) = 0
         
            [Space(10)][Header(Surface Options)][HideInInspector]_BaseMap("Texture", 2D) = "white" {}
            [HideInInspector]_TextureColor("TextureColor", Color) = (1, 1, 1, 1)
             
            [Space(10)][Header(Shader Settings)][HideInInspector] _Brightness("Brightness", Range(-1,1)) = -0.47
            [HideInInspector]_Strength("Strength/Intensity", Range(0,50)) = 6.6

            [HideInInspector] _Diffuse("Diffuse", Range(0,50)) = 1
            [HideInInspector]   _Specular("Specular", Range(0,1)) = 0
            [HideInInspector] _Metallic("Metallic", Range(0,1)) = 1.06
            [HideInInspector]_Smoothness("Smoothness", Range(0,1)) = 0.421
            [HideInInspector]  _Occlusion("Occlusion", Range(0,1)) = 0.138
            [HideInInspector]_Emission("Emission", Range(0,1)) = 0
            [HideInInspector]_Alpha("Alpha", Range(0,1)) = 1
            [HideInInspector] _ScaleAndNumberOfRings("ScaleAndNumberOfRings", Range(0.1,1)) = 0.338


            [Header(Outline Options)]
            _OutlineXScale("Outline X Scale",  Range(0,10)) = 1.07
            _OutlineYScale("Outline Y Scale", Range(0,10))= 1.07
            _OutlineZScale("Outline Z Scale", Range(0,10)) = 1.07
            _OutlineColor("Outline Color", Color) = (0,0,0,255)

            _OutlineXPosition("Outline X Position", Range(0,1)) = 0.0
            _OutlineYPosition("Outline Y Position", Range(0,1)) = 0.0
            _OutlineZPosition("Outline Z Position", Range(0,1)) = 0.0
           
        }
        SubShader
        {
            Tags
            {
                "RenderType" = "Opaque"
                "RenderPipeline" = "UniversalPipeline"
          
            }
            LOD 100
            
            Pass//main object render
            {

                     Name "ForwardLit"
                     Tags {   "LightMode" = "UniversalForward" }
           
                Lighting On
         
                HLSLPROGRAM
                // Signal this shader requires geometry programs
                #pragma prefer_hlslcc gles
                #pragma exclude_renderers d3d11_9x
                #pragma target 2.0
               // #pragma require geometry
                // Material Keywords
               // Material Keywords
                #pragma shader_feature _COLORED_SPECULAR
                #pragma shader_feature _ALPHATEST_ON
                #pragma shader_feature _ALPHAPREMULTIPLY_ON
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
            
                #pragma shader_feature _GLOSSINESS_FROM_BASE_ALPHA
                #pragma shader_feature _NORMALMAP
                #pragma shader_feature _EMISSION
           
                 // #pragma vertex vert;
                 //   #pragma fragment frag;
                 // Lighting and shadow keywords
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
                //#pragma multi_compile _ _ADDITIONAL_LIGHTS

                #define _ADDITIONAL_LIGHTS;
                #define _ADDITIONAL_LIGHTS_VERTEX;
               // #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
                  // #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
                    #pragma multi_compile _ _SHADOWS_SOFT

                //#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE


                // Unity defined keywords
               // #pragma multi_compile _DIRLIGHTMAP_COMBINED
              //  #pragma multi_compile _LIGHTMAP_ON
            
            

                // Register our functions
                #pragma vertex Vertex
              //  #pragma geometry Geometry
                #pragma fragment Fragment
              

                // Include our logic file
                #include "ToonURPShader.hlsl"    

                ENDHLSL
            }
            
           
           Pass //main shadow render
           {

                Name "ShadowCaster"
                Tags { "Queue" = "Transparent"
                    "LightMode" = "ShadowCaster" }
               //Blend One One
               //ZWrite On
               //ZTest LEqual
                HLSLPROGRAM
                // Signal this shader requires geometry programs
                        

                #pragma prefer_hlslcc gles
                #pragma exclude_renderers d3d11_9x
                #pragma target 2.0

                // Material Keywords
                #pragma shader_feature _ALPHATEST_ON
                #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

                         // GPU Instancing
                #pragma multi_compile_instancing
                //#pragma multi_compile _ DOTS_INSTANCING_ON
               // #pragma require geometry
                // #pragma vertex vert;
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
               // #pragma fragment frag;
                // This sets up various keywords for different light types and shadow settings
                #pragma multi_compile_shadowcaster

                // Register our functions
                #pragma vertex Vertex
               // #pragma geometry Geometry
                #pragma fragment Fragment  
           
                // Define a special keyword so our logic can change if inside the shadow caster pass
                #define SHADOW_CASTER_PASS
               // #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
               // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
               // #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"

                // Include our logic file
                #include "ToonURPShader.hlsl"

                ENDHLSL
           }
               
           Pass//outline pass
           {
                    Name "Outlines"
                    Cull Front
                    ZWrite On
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
        CustomEditor "ToonURPShaderGUI"
}
