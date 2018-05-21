using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class ColorScheme : IColors
{
	[SerializeField]
	private Color32[] colors;

	// Subscribe to this event to update the lookup texture whenever the scheme is modified

	public event UnityAction OnModified;

	public Color32 this[int index]
	{
		get
		{
			return colors[index];
		}

		set
		{
			if ( !value.Equals( colors[index] ) )
			{
				colors[index] = value;

				OnModified.InvokeSafely();
			}
		}
	}

	public void SetColors ( Color32[] colors )
	{
		if ( colors.Length != this.colors.Length )
		{
			throw new System.ArgumentException( "Mismatched colors array, lengths do not match." );
		}

		bool dirty = false;

		for ( int i = 0; i < colors.Length; i++ )
		{
			dirty |= !colors[i].Equals( this.colors[i] );
			this.colors[i] = colors[i];
		}

		if ( dirty )
		{
			OnModified();
		}
	}

	public int Length { get { return colors.Length; } }

	public ColorScheme ( IEnumerable<Color32> colors )
	{
		this.colors = new Color32[colors.Count()];

		int index = 0;

		foreach ( var color in colors )
		{
			this.colors[index++] = color;
		}
	}
}
