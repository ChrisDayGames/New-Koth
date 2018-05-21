using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( DynamicPalette ) )]
public class CharacterColorChanger : MonoBehaviour {

	[SerializeField]
	private DynamicPalette dynamicPalette;

	private void Reset ()
	{
		dynamicPalette = GetComponent<DynamicPalette>();
	}

	[SerializeField]
	[HideInInspector]
	private List<ColorSwap> colorsToSwap;

	private IColors original, desired;

	private void Awake ()
	{
		colorsToSwap = new List<ColorSwap>( 32 );
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="original">Typically a <see cref="Skin"/>.</param>
	/// <param name="desired">Typically a <see cref="ColorScheme"/>.</param>
	public void SetColors ( IColors original, IColors desired )
	{
		if ( original.Length != desired.Length )
		{
			throw new System.ArgumentException( "Mismatched original/desired, lengths do not match." );
		}

		// Unregister from the previous scheme
		if ( this.desired != null )
		{
			this.desired.OnModified -= HandleModified;
		}

		this.original = original;
		this.desired = desired;

		// Register with the new scheme
		if ( this.desired != null )
		{
			this.desired.OnModified += HandleModified;
		}

		// Build an array of ColorSwaps that will map the r values
		// from the original colors to the desired colors

		colorsToSwap.Clear();

		for ( int i = 0; i < original.Length; i++ )
		{
			colorsToSwap.Add( new ColorSwap { rValue = original[i].r, swapColor = desired[i] } );
		}

		SetColorsInternal();
	}

	private void HandleModified ()
	{
		for ( int i = 0; i < desired.Length; i++ )
		{
			colorsToSwap[i] = colorsToSwap[i].SetSwapColor( desired[i] );
		}
	}

	private void SetColorsInternal ()
	{
		// Actually change the colors
		dynamicPalette.ColorsToSwap = colorsToSwap;
		dynamicPalette.ChangeColor();
	}
}
