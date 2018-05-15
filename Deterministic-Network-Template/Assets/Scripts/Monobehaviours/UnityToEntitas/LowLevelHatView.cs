using UnityEngine;
using Determinism;

public class LowLevelHatView : MonoBehaviour {

	private static Color colliderColor =  Color.blue;

	[Header ( "Collider" )]
	public FixedVector2 offset;
	public FixedVector2 size;

	void OnDrawGizmos () {

		if (Application.isPlaying) return;

		colliderColor.a = 1;
		Gizmos.color = colliderColor;
		Gizmos.DrawWireCube (offset.ToVector3 (), size.ToVector3 ());

		colliderColor.a = 0.2f;
		Gizmos.color = colliderColor;
		Gizmos.DrawCube (offset.ToVector3 (), size.ToVector3 ());

	}

}
