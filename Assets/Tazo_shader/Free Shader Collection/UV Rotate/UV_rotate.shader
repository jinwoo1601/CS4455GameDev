// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Tazo/uvrotate"
{
Properties
{
_Color("BaseColor", Color) = (1,1,1,1)
_BaseTex("BaseTex", 2D) = "white" {} 
_AV("Angular velocity", float) = 1.0
} 
SubShader
{
tags{"Queue" = "Transparent" "RenderType" = "Transparent" }
Blend SrcAlpha OneMinusSrcAlpha
Cull off
Pass
{ 
CGPROGRAM
#pragma vertex vs
#pragma fragment ps 
float4 _Color;
float _AV;
sampler2D _BaseTex; 
struct VS_IN
{
float4 pos : POSITION;
float4 uv : TEXCOORD0;
};
struct VS_OUT
{
float4 pos:POSITION;
float4 uv:TEXCOORD0;
}; 
VS_OUT vs(VS_IN input)
{
VS_OUT output;
output.pos = UnityObjectToClipPos(input.pos);
output.uv = input.uv;
return output;
} 
float4 ps(VS_OUT input):COLOR
{
float2 uv = input.uv.xy - float2(0.5, 0.5);
uv = float2(uv.x*cos(_Time.y*_AV) - uv.y*sin( _Time.y*_AV),uv.x*sin(_Time.y*_AV) + uv.y *cos(_Time.y*_AV) );
uv += float2(0.5, 0.5);
float4 cc = tex2D(_BaseTex, uv) * _Color;
return cc;
} 
ENDCG
}
}
}