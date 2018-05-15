using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerManager : MonoBehaviour {

	public ParticleSystem[] systems; 

	private Vector3 lastPosition;
	private Vector2 direction;

	void Update () {

		direction.x = transform.position.x - lastPosition.x;
		direction.y = transform.position.y - lastPosition.y;

		//get the angle of the throw
		float angle = Mathf.Atan2(-direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;

		//add 180 to compensate for the initial offset
		angle += 180;

		var fireEmitParams = new ParticleSystem.EmitParams();
		fireEmitParams.rotation3D = new Vector3(0, 0, angle);

		foreach (ParticleSystem ps in systems)
			ps.Emit(fireEmitParams, 1);

		lastPosition = transform.position;

	}

}
