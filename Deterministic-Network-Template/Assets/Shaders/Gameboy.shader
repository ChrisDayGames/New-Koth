Shader "Unlit/Gameboy"
{
	Properties
	{

		_MainTex ("Texture", 2D) = "white" {}
		_Lookup ("Lookup", 2D) = "white" {}
		_Spread ("Spread", Range (0, 1)) = 1

	}
	SubShader
	{
		Tags { "RenderType"="transparent" }

		Pass
		{

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _Lookup;
			float _Spread;
			float _Min;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;

			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				float test = col.rgb;

				if (test == 0)
					return (0.91,0.95,0.95,1) * col.a;

				fixed x = col.rgb  + (col.rgb * _Spread);
				return tex2D(_Lookup, float2(x, 0)) * col.a;

			}

			ENDCG

		}
	}
}
