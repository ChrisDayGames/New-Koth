using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Determinism;

public class LowLevelPlayerView : MonoBehaviour {

	private static Color colliderColor =  Color.blue;
	private static Color pickUpColor =  Color.green;

	[Header ( "Collider" )]
	public FixedVector2 offset;
	public FixedVector2 size;

	[Header ( "Pick Up Ranger" )]
	public FixedVector2 followPoint;
	public long pickUpRadius;


	void OnDrawGizmos () {

		if (Application.isPlaying) return;

		colliderColor.a = 1;
		Gizmos.color = colliderColor;
		Gizmos.DrawWireCube (offset.ToVector3 (), size.ToVector3 ());

		colliderColor.a = 0.2f;
		Gizmos.color = colliderColor;
		Gizmos.DrawCube (offset.ToVector3 (), size.ToVector3 ());

		pickUpColor.a = 1;
		Gizmos.color = pickUpColor;
		Gizmos.DrawCube (followPoint.ToVector3 (), Vector3.one * 0.1f);
		Gizmos.DrawWireSphere (followPoint.ToVector3 (), pickUpRadius.ToFloat () / CharacterBlueprint.scale.ToFloat ());

		pickUpColor.a = 0.2f;
		Gizmos.DrawWireSphere (followPoint.ToVector3 (), pickUpRadius.ToFloat () / CharacterBlueprint.scale.ToFloat ());

	}

}
