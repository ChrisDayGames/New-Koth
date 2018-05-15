Shader "Sprites/Ultimate"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (0,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 1

		_SwapTex("Color Data", 2D) = "transparent" {}
		_OverrideColor ("Override", Color) = (1,1,1,0)
		[MaterialToggle] _InvertColors ("Invert", Float) = 1

	}

	SubShader
	{

		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
		
			fixed4 _Color;
			fixed4 _OverrideColor;
			float _InvertColors;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _SwapTex;

			float4 _MainTex_TexelSize;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv) * _Color;
				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{

				half4 c = SampleSpriteTexture (IN.texcoord);
				half4 swapCol = tex2D(_SwapTex, float2(c.r, 0));

				half4 final = lerp(c, swapCol, swapCol.a);
    			final = lerp(final, _OverrideColor, _OverrideColor.a);

    			//final.rgb = _InvertColors + ((-2 * _InvertColors + 1) * final.rgb);
    			final.rgb =  abs (_InvertColors - final.rgb);

    			final.a = c.a;
 
    			return final;

			}

			ENDCG

		}

	}

	Fallback "Sprites/Default"

}