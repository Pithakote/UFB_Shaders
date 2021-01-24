// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Pos("LOLPos",vector) = (0,0,0,0)

    _Brightness("Brightness", Range(0,1)) = 0.3
    _Strength("Strength", Range(0,1)) = 0.3

              }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" "Queue" = "Geometry" }
        LOD 100

        Pass//draw this for the base lighting property
        {
            
            Name "ForwardLit"
            Tags {"LightMode" = "UniversalForward"}
           // Cull Back

            HLSLPROGRAM
#pragma prefer_hlslcc gles
#pragma exclude_renderers d3d11_9x
#pragma target 2.0
#pragma require geometry

#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
#pragma multi_compile _ _ADDITIONAL_LIGHTS
#pragma multi_compile _ _ADDITIONAL_LIGHTS_SHADOWS
#pragma multi_compile _ _SHADOWS_SOFT

/*
#pragma vertex Vertex
#pragma fragment Fragment
*/
           // Tags {"LightMode" = "ForwardBase"}
         //   Blend One One
           // CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
           // #pragma multi_compile_fog

            #include "UnityCG.cginc"
          // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;//local position
                float2 uv : TEXCOORD0;
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
              //  UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;//local positoion//SV_ is important to make it work in DirectX hardware like Playstation
                float4 worldPos : TEXCOORD1;
                half3 worldNormal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Brightness;
            float _Strength;
            float3 objectToOtherLightSources;
            float Toon(float3 normal, float3 lightDir)
            {
                float NdotL = max(0.0,dot((normal), (lightDir)));

                return round(NdotL/0.3);//goes from 0 to 0.3, 0.3 to 0.6, 0.6 to 0.9 and 0.9 to 1. So 4 partations
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);//get's the world positiond
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);//takes the local normal and converts to world space/ 
                              //   UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
             //   UNITY_APPLY_FOG(i.fogCoord, col);
                if (_WorldSpaceLightPos0.w == 0.0)//for directional light
                {
                    objectToOtherLightSources = normalize(_WorldSpaceLightPos0.xyz);
               
                
                }
                else
                {
                
                    objectToOtherLightSources = _WorldSpaceLightPos0.xyz - i.worldPos;
                    float distance = length(objectToOtherLightSources);
                    _Strength = 1 / distance;
                    objectToOtherLightSources = normalize(objectToOtherLightSources);
                }
                objectToOtherLightSources = normalize(_WorldSpaceLightPos0.xyz);
                col *= Toon(i.worldNormal, (objectToOtherLightSources)) * _Strength + _Brightness;
                // col *= round(max(0.0,dot(normalize(_WorldSpaceLightPos0.xyz), normalize(i.worldNormal)))/0.7) * _Strength + _Brightness;
                //  col.a = 0.1f;
                return col;
            }
            ENDHLSL
        }
           
       
    }
}
