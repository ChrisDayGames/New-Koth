Shader "Sprites/Shadow"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (0,1,1,1)
		_SliceAmountLeft ("Slice Amount", Range(0.0, 1.0)) = 0
		_SliceAmountRight ("Slice Amount", Range(0.0, 1.0)) = 0
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 1
		_Channel("Channel", int) = 1
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

			Stencil {
				Ref [_Channel]
				Comp equal
				Pass keep
			}

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
			float _SliceAmountLeft;
			float _SliceAmountRight;

			float4 _MainTex_TexelSize;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv) * _Color;
				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{

				clip(IN.texcoord.x - _SliceAmountLeft);
				clip(_SliceAmountRight - IN.texcoord.x);

				half4 c = SampleSpriteTexture (IN.texcoord);
    			return c;

			}

			ENDCG

		}

	}

	Fallback "Sprites/Default"

}