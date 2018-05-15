//Stencil Comparisons
//0 - Always (?)
//1 - Never
//2 - Less
//3 - Equal
//4 - LEqual
//5 - Greater
//6 - NotEqual
//7 - GEqual
//8 - Always 

//Stencil Operations
//0 - Keep (?)
//1 - Zero
//2 - Replace
//3 - IncrSat
//4 - DecrSat
//5 - Invert
//6 - IncrWrap
//7 - DecrWrap


Shader "Sprites/Stencil"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (0,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 1
		_Channel("Channel", int) = 1

		[KeywordEnum(Disabled, Never, Less, Equal, LEqual, Greater, NotEqual, GEqual, Always)]
		_Stencil_Comp("Stencil Comparison", Float) = 8

		[KeywordEnum(Keep, Zero, Replace, IncrSat, DecrSat, Invert, IncrWrap, DecrWrap)]
		_Stencil_Op("Stencil Operation", Float) = 0

	}

	SubShader
	{

		Tags
		{ 
			"IgnoreProjector"="True" 
			"RenderType"="Opaque" 
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
				Comp [_Stencil_Comp]
				Pass [_Stencil_Op]
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

			float4 _MainTex_TexelSize;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv) * _Color;
				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{

				half4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				
    			return c;

			}

			ENDCG

		}

	}

	Fallback "Sprites/Default"

}