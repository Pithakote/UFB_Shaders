#ifndef OUTLINES_INCLUDED
#define OUTLINES_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes 
{
	float4 positionOS : POSITION;
	float3 normalOS   : NORMAL;
	float4 tangentOS : TANGENT;
	float2 uv : TEXCOORD0;
};

struct VertexOutput
{
	float3 positionWS : TEXCOORD0;//position in world space

	float4 positionCS : SV_POSITION;
	float2 uv : TEXCOORD1;
	float3 normalWS : TEXCOORD2;
};
float _OutlineThickness;
float4 _OutlineColor;

VertexOutput Vertex(Attributes input)
{
	VertexOutput output = (VertexOutput)0;

	float3 normalOS = input.normalOS;

	//extruding the object along the normal to make it a little bigger
	//float3 posOS = input.positionOS.xyz + input.normalOS * _Thickness;
	float3 posOS = input.positionOS.xyz  * _OutlineThickness;
	input.positionOS.xyz *= _OutlineThickness;

	//output.positionCS = GetVertexPositionInputs(input.positionOS).positionCS;
	VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
	output.positionWS = vertexInput.positionWS;
	output.positionCS = vertexInput.positionCS;


	VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
	output.normalWS = (normalInput.normalWS);


	//  output.positionCS = CalculatePositionCSWithShadowCasterLogic(output.positionCS, output.normalWS);

	//output.uv = TRANSFORM_TEX(input.uv, _Color);
	return output;
}

float4 Fragment(VertexOutput input) : SV_Target
{
	return _OutlineColor;
}
#endif