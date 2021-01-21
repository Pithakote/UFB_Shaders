Shader "Custom/ToonURPShader"
{
    Properties
    {
        _BaseMap("Texture", 2D) = "white" {}
        _BaseColor("Color", Color) = (1, 1, 1, 1)
        _Brightness("Brightness", Range(-1,1)) = 0.3
        _Strength("Strength", Range(0,2)) = 0.3
        _TriNormalHeight("TriNormalHeight", Range(0,0.005)) = 0.3
    }
    SubShader
    {
        Tags 
        {
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
           // "Queue" = "Geometry"
        }
       // LOD 100

       Pass//main object render
       {

                Name "ForwardLit"
                Tags { "LightMode" = "UniversalForward" }
               // Cull Back
                 LOD 100
                HLSLPROGRAM
                // Signal this shader requires geometry programs
                #pragma prefer_hlslcc gles
                #pragma exclude_renderers d3d11_9x
                #pragma target 2.0
               // #pragma require geometry

             // #pragma vertex vert;
             //   #pragma fragment frag;
                // Lighting and shadow keywords
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
                #pragma multi_compile _ _ADDITIONAL_LIGHTS
               #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
                #pragma multi_compile _ _SHADOWS_SOFT

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
            Tags { "Queue" = "Transparent" "LightMode" = "ShadowCaster" }
           ZWrite On
    ZTest LEqual
            HLSLPROGRAM
            // Signal this shader requires geometry programs
            #pragma vertex Vertex
           // #pragma geometry Geometry
            #pragma fragment Fragment           

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
           // #pragma fragment frag;
            // This sets up various keywords for different light types and shadow settings
            #pragma multi_compile_shadowcaster

            // Register our functions
          
           
            // Define a special keyword so our logic can change if inside the shadow caster pass
            #define SHADOW_CASTER_PASS
           // #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
   // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
   // #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"

            // Include our logic file
            #include "ToonURPShader.hlsl"

            ENDHLSL
       }
      
    }
}
