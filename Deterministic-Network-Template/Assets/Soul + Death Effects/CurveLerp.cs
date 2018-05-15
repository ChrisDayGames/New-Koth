using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLerp : FXBehaviour {


	[Header ( "Curve Lerp" )]
	public int frameDuration = 10;
	public float frameDelta {
		get {return 1f / (float) frameDuration;}
	}
	public float endTime = 1f;
    public AnimationCurve xCurve;
    public AnimationCurve yCurve;


	protected Vector3 start;
	protected Transform end;

	private Vector3 lastPosition = Vector3.zero;
	private Vector3 currentPosition = Vector3.zero;

    float t = 0;
    bool reachedTarget = false;

	public override void FixedExecute() {

		if (!reachedTarget)
			MoveToTarget ();

    }

	public override void Execute (float alpha) {

		if (!reachedTarget)
			transform.position = Vector3.Lerp (
				lastPosition,
				currentPosition,
				alpha
			);

	}

	public override void Initialize (LogicEntity e, Transform owner) {

		Vector3 hatPosition;

		if (!e.hasPosition && !e.hasHat) {

			gameObject.SetActive (false);
			return;
		}

		hatPosition = Contexts.sharedInstance.logic.GetEntityWithId (e.hat.entityID).position.value.ToVector3 ();
		end = owner;

		Init (hatPosition, owner);

	}

	public virtual void Init (Vector3 _start, Transform _end) {

		start = _start;
		end = _end;

		transform.position = start;
		lastPosition = start;
		currentPosition = start;

		gameObject.SetActive (true);
		reachedTarget = false;
		t = 0;

	}

	protected virtual void MoveToTarget () {

		t += frameDelta;
		Mathf.Clamp01(t);

		lastPosition = currentPosition;
		currentPosition.x = Mathf.LerpUnclamped(start.x, end.position.x, xCurve.Evaluate(t));
		currentPosition.y = Mathf.LerpUnclamped(start.y, end.position.y, yCurve.Evaluate(t));

		if (t >= 1)
			OnReachDestination ();
		
	}

    protected virtual void OnReachDestination () {

		reachedTarget = true;
		transform.position = currentPosition;
		Invoke ("DelayedOnReachDestination", endTime);

    }


	protected virtual void DelayedOnReachDestination () {}

}
