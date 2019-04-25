// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Tazo/Saturation_Brightness"
{
Properties
{
_Color("BaseColor", Color) = (1,1,1,1)
_BaseTex("Base(RGB)", 2D) = "white" {}
_Saturation("Saturation", range(0.0,1.0)) = 1.0
_Brightness("Brightness", range(0.0,2.0)) = 1.0 
}

SubShader
{
tags{"Queue" = "Geometry" "RenderType" = "Opaque" }
cull back

Pass
{

CGPROGRAM
#pragma vertex vs
#pragma fragment ps

#include "UnityCG.cginc"
float4 _Color;
float _Saturation;
float _Brightness;
sampler2D _BaseTex;
float4 _BaseTex_ST;
struct VS_OUT
{
float4 position:POSITION;
float2 uv_base:TEXCOORD0;

};


VS_OUT vs(appdata_base input)
{
VS_OUT output; 

output.position = UnityObjectToClipPos(input.vertex);

output.uv_base = TRANSFORM_TEX(input.texcoord, _BaseTex);

return output;
} 
float4 ps(VS_OUT input):COLOR
{ 

float3 rgb = tex2D(_BaseTex, input.uv_base)*_Color;
float4 ee = tex2D(_BaseTex, input.uv_base)*_Color;
float3 table = float3(0.3, 0.59, 0.11);
float4 cc = lerp(dot(rgb, table),ee,_Saturation);
cc *= _Brightness;
return cc;

}

ENDCG
}
}
}