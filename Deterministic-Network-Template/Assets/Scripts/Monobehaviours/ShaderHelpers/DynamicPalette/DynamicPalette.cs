using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( SpriteRenderer ) )]
public class DynamicPalette : MonoBehaviour
{
	[SerializeField]
	[HideInInspector]
	private List<ColorSwap> m_ColorsToSwap = new List<ColorSwap>( 32 );

	public List<ColorSwap> ColorsToSwap
	{
		get
		{
			return m_ColorsToSwap;
		}

		set
		{
			m_ColorsToSwap = value;
		}
	}

	Texture2D colorSwapTex;
	Color32[] spriteColors;
	SpriteRenderer spriteRenderer;

	public void Init ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void InitColorSwapTex ()
	{
		Texture2D _colorSwapTex = new Texture2D( 256, 1, TextureFormat.RGBA32, false, false );

		_colorSwapTex.filterMode = FilterMode.Point;
		_colorSwapTex.SetPixels32( 0, 0, _colorSwapTex.width, 1, new Color32[_colorSwapTex.width] );
		_colorSwapTex.Apply();

		spriteRenderer.material.SetTexture( "_SwapTex", _colorSwapTex );

		spriteColors = new Color32[_colorSwapTex.width];
		colorSwapTex = _colorSwapTex;
	}

	public void ResetColors ()
	{
		for ( int i = 0; i < colorSwapTex.width; ++i )
		{
			colorSwapTex.SetPixel( i, 0, new Color32( 0, 0, 0, 0 ) );
		}

		colorSwapTex.Apply();
	}

	public void SwapPalette ()
	{
		spriteColors = new Color32[colorSwapTex.width];

		for ( int i = 0; i < m_ColorsToSwap.Count; i++ )
		{
			spriteColors[m_ColorsToSwap[i].rValue] = m_ColorsToSwap[i].swapColor;
		}

		for ( int i = 0; i < colorSwapTex.width; ++i )
		{
			colorSwapTex.SetPixel( i, 0, spriteColors[i] );
		}

		colorSwapTex.Apply();
	}

	public void ChangeColor ()
	{
		InitColorSwapTex();
		SwapPalette();
	}
}