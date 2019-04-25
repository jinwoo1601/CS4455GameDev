// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Tazo/heightmapShader"
{
	Properties
	{
	_Color("BaseColor", Color) = (1,1,1,1)
		_BaseTex("Base(RGB)", 2D) = "white" {}
		_HeightTex("Height(RGB)", 2D) = "white" {}
		_Height("Height", range(0.0,2.0)) = 1.0		
	}
	
	SubShader
	{
		tags{"Queue" = "Geometry" "RenderType" = "Opaque" }
		cull off
		
		Pass
		{
			
			CGPROGRAM
			#pragma vertex vs
			#pragma fragment ps
			#pragma target 3.0
			#pragma glsl
			#include "UnityCG.cginc"
			float4 _Color;
			sampler2D _BaseTex;
			sampler2D _HeightTex;
			float _Height;
		
			struct VS_OUT
			{
				float4 pos:POSITION;
				float2 uv_base:TEXCOORD0;
			};
			float4 _BaseTex_ST;
						
			VS_OUT vs(appdata_base input)
			{
				VS_OUT output;			
				float4 hh = tex2Dlod(_HeightTex ,input.texcoord);
				float3 newposition = input.vertex.xyz + input.normal * hh  * _Height;
				output.pos = UnityObjectToClipPos(float4(newposition.xyz, 1.0));
				output.uv_base = TRANSFORM_TEX(input.texcoord, _BaseTex);			
				return output;
			}			
			float4 ps(VS_OUT input):COLOR
			{	
				float4 cc = tex2D(_BaseTex ,input.uv_base) * _Color;
				return cc;
			}
			
			ENDCG
		}
	}
}