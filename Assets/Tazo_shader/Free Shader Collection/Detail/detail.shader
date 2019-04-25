// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Tazo/detail"
{
	Properties
	{
		_Color("BaseColor", Color) = (1,1,1,1)
		_BaseTex("Base(RGB)", 2D) = "white" {}
		
		_DetailTex("Detail(RGB)", 2D) = "white" {}
		
		
	}
	
	SubShader
	{
		tags{"Queue" = "Geometry" "RenderType" = "Opaque" }
		
		
		Pass
		{
			
			CGPROGRAM
			#pragma vertex vs
			#pragma fragment ps
			#include "UnityCG.cginc"
			
			
			sampler2D _BaseTex;
			sampler2D _DetailTex;
			
			struct VS_OUT
			{
				float4 pos:POSITION;
				float2 uv_base:TEXCOORD0;
				float2 uv_detail:TEXCOORD1;
			};
			float4 _Color;
		
			float4 _BaseTex_ST;
			float4 _DetailTex_ST;
			
			VS_OUT vs(appdata_base input)
			{
				VS_OUT output;
				output.pos = UnityObjectToClipPos(input.vertex);
				output.uv_base = TRANSFORM_TEX(input.texcoord, _BaseTex);
				output.uv_detail = TRANSFORM_TEX(input.texcoord,_DetailTex);
				return output;
			}
			
			float4 ps(VS_OUT input):COLOR
			{	
				float4 cc;
				float4 bb = tex2D(_BaseTex ,input.uv_base);
				float4 dd = tex2D(_DetailTex,input.uv_detail);
				cc = bb*dd*_Color;
				return cc;
			}
			
			ENDCG
		}
	}
}