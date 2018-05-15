// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Sprites/Fog"
{
	Properties
	{

		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_FogTex ("Fog Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_OverrideColor ("Override", Color) = (1,1,1,0)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0

	}

	SubShader
	{
		Tags
		{ 
			"Queue" = "Transparent"
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest Off
	
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members uvproj)
#pragma exclude_renderers d3d11
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
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
				float2 worldPos : TEXCOORD1;
			};
			
			fixed4 _Color;
			fixed4 _OverrideColor;
			fixed2 _ScreenUV;

			sampler2D _MainTex_ST;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;

				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
				
				OUT.worldPos = (OUT.vertex.xy / OUT.vertex.w + 1) * 0.5;

				return OUT;

			}

			sampler2D _MainTex;
			sampler2D _FogTex;
			sampler2D _AlphaTex;
			sampler2D _SwapTex;
			float4 _MainTex_TexelSize;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);
				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				
				half4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
    			half4 final = lerp(c, tex2D(_FogTex, IN.worldPos) * _OverrideColor, _OverrideColor.a);
		        final.a = c.a;
 				
    			return final;

			}

		ENDCG

		}

	}

}
