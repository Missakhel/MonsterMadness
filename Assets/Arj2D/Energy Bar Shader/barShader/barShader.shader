// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Bar/Bar3D" 
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,0.5)
		_DeltaColor ("Delta Color", Color) = (1,1,1,0.5)
		_CutValue ("Cut value", Range (0, 0.99)) = 0.99
		_DeltaCutValue ("Delta Cut value", Range (0, 0.99)) = 0.99
		_MainTex ("Main Texture", 2D) = ""
		_DeltaTex ("Delta Texture", 2D) = ""
		_Gradient ("Gradient", 2D) = ""
		_Border ("Border", 2D) = ""
    }
	SubShader 
	{
      	Blend SrcAlpha OneMinusSrcAlpha
      	Tags {Queue=Transparent}
       
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			float4 		_Color;
			float 		_CutValue;
			sampler2D 	_MainTex;
			
			float4 		_DeltaColor;
			float 		_DeltaCutValue;
			sampler2D 	_DeltaTex;
			
			sampler2D 	_Gradient;
			sampler2D 	_Border;
			
			uniform float4 _MainTex_ST;
			uniform float4 _DeltaTex_ST;
			uniform float4 _Gradient_ST;
			uniform float4 _Border_ST;
			
			struct v2f {
			    float4 pos : SV_POSITION;
				float2  UV_MainTex : TEXCOORD0;
				float2  UV_Gradient : TEXCOORD1;
				float2  UV_Border : TEXCOORD2;
				float2  UV_DeltaTex : TEXCOORD3;
			};
			
			v2f vert (appdata_base v)
			{
			    v2f o;
			    o.pos = UnityObjectToClipPos (v.vertex);
			    o.UV_MainTex = TRANSFORM_TEX (v.texcoord, _MainTex);
				o.UV_Gradient = TRANSFORM_TEX (v.texcoord, _Gradient);
				o.UV_Border = TRANSFORM_TEX (v.texcoord, _Border);
				o.UV_DeltaTex = TRANSFORM_TEX (v.texcoord, _DeltaTex);
			    return o;
			}
	
			half4 frag (v2f i) : COLOR
			{
				half4 borderColor = tex2D(_Border, i.UV_Border);
				half4 barColor = float4(0, 0, 0, 0);
				if (tex2D(_Gradient, i.UV_Gradient).x < _CutValue)
			    	 barColor = tex2D(_MainTex, i.UV_MainTex) * _Color;
			   	else if (tex2D(_Gradient, i.UV_Gradient).x < _DeltaCutValue)
			   		barColor = tex2D(_DeltaTex, i.UV_DeltaTex) * _DeltaColor;
			    return barColor *  (1 - borderColor.a) + borderColor * borderColor.a;
			}
			ENDCG
		}
	}
}