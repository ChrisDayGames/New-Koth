using UnityEngine;
using UnityEditor;
using Determinism;

[CustomPropertyDrawer( typeof( long ) )]
public class FixedDrawer : PropertyDrawer {
	private static GUIStyle style;

	static FixedDrawer ()
	{
		style = new GUIStyle( EditorStyles.miniLabel );
		style.alignment = TextAnchor.UpperRight;
	}

	public override void OnGUI ( Rect position, SerializedProperty property, GUIContent label ) {

		// cache this so we can reset it at the end
		bool showMixedValue = EditorGUI.showMixedValue;

		if (property.hasMultipleDifferentValues)
		{
			// show a dash instead of numbers in the FloatField
			EditorGUI.showMixedValue = true;
		}

		// start observing GUI to see if user does something
		EditorGUI.BeginChangeCheck();

		var value = FixedMath.Create( EditorGUI.FloatField ( position, label, property.longValue.ToFloat() ) );

		// if the user messed with the FloatField
		if ( EditorGUI.EndChangeCheck() )
		{
			// set the propery (including multi-value) to the new value
			property.longValue = value;
		}

		// draw the long value in the bottom right corner
		GUI.Label( position, new GUIContent( property.longValue.ToString()), style );

		// reset the showMixedValue flag (maybe not necessary)
		EditorGUI.showMixedValue = showMixedValue;
	}

}