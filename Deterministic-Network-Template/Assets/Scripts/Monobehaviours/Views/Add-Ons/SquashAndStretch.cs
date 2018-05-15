using UnityEngine;

public class SquashAndStretch : ManagedBehaviour {

	public float magnitude = 3f;
	public float lowPassFilter = 0.01f;
	public float maxStretch = 0.3f;

	private Vector3 lastAcceleration;
	private Vector3 acceleration;

	private Vector3 lastVelocity;
	private Vector3 velocity;

	private Vector3 lastPosition;
	private Vector3 position;

	private Vector3 referenceScale = Vector3.one;

	public void ChangeReferenceScale (Vector3 _referenceScale) {

		this.referenceScale = _referenceScale;

	}

	public override void Execute (float alpha) {

		Squash (

			Vector3.Lerp (

				lastAcceleration,
				acceleration,
				alpha

			)

		);
	
	}

	public override void FixedExecute () {

		//update position
		lastPosition = position;
		position = transform.position;

		//update velocity
		lastVelocity = velocity;
		velocity = (position - lastPosition);

		lastAcceleration = acceleration;	
		acceleration = (velocity - lastVelocity);
	
	}

	private void Squash (Vector3 acceleration) {

		float squashValue = Mathf.Clamp (((Mathf.Abs(acceleration.x)) - Mathf.Abs(acceleration.y)) * magnitude, -maxStretch, maxStretch);
		if (Mathf.Abs (squashValue) < lowPassFilter) 
			squashValue = 0;

		Vector3 squashVector = new Vector3 (squashValue, -squashValue, 0);
		transform.localScale = (referenceScale + squashVector);
		
	}

}
