// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Tazo/reflect"
{
	Properties
	{
		_Color("BaseColor", Color) = (0.5,0.5,0.5,0.5)
		_BaseTex("Base(RGB)", 2D) = "white" {}
		_CubeTex("Cube(RGB)", Cube) = ""  {TexGen CubeReflect }		
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
			
			#include "UnityCG.cginc"
			float4 _Color;
			sampler2D _BaseTex;
			samplerCUBE _CubeTex;
		
			struct VS_OUT
			{
				float4 position:POSITION;
				float3 w_position:TEXCOORD0;
				float3 w_normal:TEXCOORD1;
				float2 uv:TEXCOORD2;
			};
			
						
			VS_OUT vs(appdata_base input)
			{
				VS_OUT output;			
				
				output.position = UnityObjectToClipPos(input.vertex);
				output.w_position = mul(unity_ObjectToWorld,input.vertex).xyz;
				output.w_normal = mul(unity_ObjectToWorld,float4(input.normal,1)).xyz;
					output.uv = input.texcoord;
				return output;
			}		
			float4 ps(VS_OUT input):COLOR
			{	
				float2 uv = input.uv;
    				float3 ray = normalize(input.w_position - _WorldSpaceCameraPos);
    				float3 ww = normalize(input.w_normal);
				float3 reflection = reflect(ray, ww); 
				float4 tt = tex2D(_BaseTex, uv)*_Color;
				float4 cc = texCUBE(_CubeTex, reflection);
				float4 ee = (tt +cc);
				return ee;
				
			}
			
			ENDCG
		}
	}
}