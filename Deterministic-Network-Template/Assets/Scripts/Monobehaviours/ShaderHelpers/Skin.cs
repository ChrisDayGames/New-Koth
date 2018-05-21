using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;

[CreateAssetMenu]
public class Skin : ScriptableObject, IColors
{
	[SerializeField]
	private Texture2D[] textures = new Texture2D[0];

	//[ReadOnly]
	[SerializeField]
	private Color32[] colors = new Color32[0];

	[ReadOnly]
	[SerializeField]
	private byte[] rValues = new byte[0];

	[SerializeField]
	private ColorScheme[] builtInColorSchemes = new ColorScheme[0];

	// when loading the game, look in the Resources/ColorSchemes folder,
	// and check for files with names that match each skin's name, if the file exists,
	// assign it to the skin's customColorScheme field

	[SerializeField]
	private ColorScheme customColorScheme;

	public ColorScheme CustomColorScheme
	{
		get
		{
			return customColorScheme;
		}

		set
		{
			value = customColorScheme;
		}
	}

	#region IColors implementation
	int IColors.Length
	{
		get
		{
			return colors.Length;
		}
	}

	Color32 IColors.this[int index]
	{
		get
		{
			return colors[index];
		}
	}

	event UnityAction IColors.OnModified
	{
		add {}
		remove {}
	}
	#endregion

#if UNITY_EDITOR
	[ReadOnly]
	[ShowInInspector]
	[ShowIf( "hasConflictingColors" )]
	[ValidateInput( "ValidateConflictingColors", defaultMessage: "One or more colors in this skin's texture set have the same R value.", messageType: InfoMessageType.Error )]
	[Tooltip( "Colors whose R value are the same." )]
	private Dictionary<byte,List<Color32>> conflictingColors;

	private bool hasConflictingColors { get { return conflictingColors != null && conflictingColors.Count > 0; } }

	private bool ValidateConflictingColors ()
	{
		return hasConflictingColors;
	}

	[Button]
	public void ScanTextures ()
	{
		try
		{
			rValues = SkinUtility.GetRValues( textures ).ToArray();
			colors = SkinUtility.GetColors( textures ).ToArray();

			conflictingColors = SkinUtility.GetConflicts( colors );
		}
		catch ( System.Exception e )
		{
			throw e;
		}
	}
#endif
}
