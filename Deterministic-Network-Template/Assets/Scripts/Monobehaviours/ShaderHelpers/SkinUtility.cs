using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
#endif

[System.Serializable]
public class MappingRule
{
	public byte key;
	public Color32 value;

	public MappingRule( byte key, Color32 value )
	{
		this.key = key;
		this.value = value;
	}
}

[System.Serializable]
public class MappingRulesList : List<MappingRule> { }

[System.Serializable]
public class Color32List : List<Color32> { }

public static class SkinUtility
{
	public static List<MappingRule> AsMappingRules ( this IEnumerable<KeyValuePair<byte, Color32>> pairs )
	{
		var list = new List<MappingRule>();

		foreach ( var pair in pairs )
		{
			list.Add( new MappingRule( pair.Key, pair.Value ) );
		}

		return list;
	}

	public static Dictionary<byte, HashSet<Color32>> GetColorsPerRValue ( params Texture2D [] textures )
	{
		var mappings = new Dictionary<byte, HashSet<Color32>>();

		foreach ( var texture in textures )
		{
			var pixels = texture.GetPixels32();

			for ( int i = 0; i < pixels.Length; i++ )
			{
				var pixel = pixels[i];

				if ( !pixel.Equals( clear ) )
				{
					var key = pixel.r;

					HashSet<Color32> value;

					if ( !mappings.TryGetValue( key, out value ) )
					{
						mappings.Add( key, value = new HashSet<Color32>() );
					}

					value.Add( pixel );
				}
			}
		}

		return mappings;
	}

	private static readonly Color32 clear = new Color32( 0, 0, 0, 0 );

	public static List<MappingRule> GetImplicitRules ( params Texture2D [] textures )
	{
		var mappings = new Dictionary<byte, Color32>();

		foreach ( var texture in textures )
		{
			var pixels = texture.GetPixels32();

			for ( int i = 0; i < pixels.Length; i++ )
			{
				var pixel = pixels[i];

				if ( !pixel.Equals( clear ) )
				{
					var key = pixel.r;

					mappings[key] = pixel;
				}
			}
		}

		return mappings.AsMappingRules();
	}

	public static bool ContainsConflicts ( this IEnumerable<KeyValuePair<byte, HashSet<Color32>>> colorsPerRValue )
	{
		foreach ( var pair in colorsPerRValue )
		{
			if ( pair.Value.Count > 1 )
			{
				return true;
			}
		}

		return false;
	}

	public static HashSet<byte> GetRValues ( params Texture2D[] textures )
	{
		var keys = new HashSet<byte>();

		foreach ( var texture in textures )
		{
			var pixels = texture.GetPixels32();

			for ( int i = 0; i < pixels.Length; i++ )
			{
				if ( !pixels[i].Equals( clear ) )
				{
					keys.Add( pixels[i].r );
				}
			}
		}

		return keys;
	}

	public static HashSet<Color32> GetColors ( params Texture2D[] textures )
	{
		var colors = new HashSet<Color32>();

		foreach ( var texture in textures )
		{
			var pixels = texture.GetPixels32();

			for ( int i = 0; i < pixels.Length; i++ )
			{
				if ( !pixels[i].Equals( clear ) )
				{
					colors.Add( pixels[i] );
				}
			}
		}

		return colors;
	}

	public static IEnumerable<MappingRule> GetUnusedRules ( this IEnumerable<MappingRule> rules, params Texture2D[] textures )
	{
		var keys = GetRValues( textures );

		foreach ( var rule in rules )
		{
			if ( !keys.Contains( rule.key ) )
			{
				yield return rule;
			}
		}
	}

	public static Dictionary<byte,List<Color32>> GetConflicts ( IEnumerable<Color32> colors )
	{
		var dictionary = new Dictionary<byte, List<Color32>>();

		foreach ( var color in colors )
		{
			List<Color32> list;

			if ( !dictionary.TryGetValue( color.r, out list ) )
			{
				list = new List<Color32>( 1 );
			}

			list.Add( color );
		}

		var keys = dictionary.Keys.ToArray();

		foreach ( var key in keys )
		{
			if ( dictionary[key].Count < 2 )
			{
				dictionary.Remove( key );
			}
		}

		return dictionary;
	}

#if UNITY_EDITOR
	/// <summary>
	/// Editor only.
	/// </summary>
	public static IEnumerable<Sprite> GetSprites ( AnimatorController controller )
	{
		if ( controller != null )
		{
			var sprites = new HashSet<Sprite>();

			var clips = controller.animationClips;

			foreach ( var clip in clips )
			{
				if ( clip != null )
				{
					foreach ( var binding in AnimationUtility.GetObjectReferenceCurveBindings( clip ) )
					{
						var keyframes = AnimationUtility.GetObjectReferenceCurve( clip, binding );

						foreach ( var keyframe in keyframes )
						{
							var sprite = (Sprite) keyframe.value;

							if ( sprite != null )
							{
								sprites.Add( sprite );
							}
						}
					}
				}
			}

			return sprites;
		}

		return Enumerable.Empty<Sprite>();
	}

	/// <summary>
	/// Editor only.
	/// </summary>
	public static IEnumerable<Texture> GetTextures ( IEnumerable<Sprite> sprites )
	{
		var textures = new HashSet<Texture>();

		foreach ( var sprite in sprites )
		{
			if ( sprite != null )
			{
				textures.Add( sprite.texture );
			}
		}

		return textures;
	}
#endif
}
