using Entitas;
using Determinism;
using Entitas.Unity;
using UnityEngine;

public class View : ManagedBehaviour, IView, IPositionListener, IRotationListener, IScaleListener, IDirectionListener {

	public bool isFollowTarget = false;
	public bool isSquishy = true;

	public Animator anim;
	public SpriteRenderer sr;

	private Vector3 lastPosition;
	private Vector3 position;
	private float lastRotation;
	private float rotation;
	private Vector3 lastScale;
	private Vector3 scale;

	public virtual void Link(IEntity entity, IContext context) {

		var e = (LogicEntity) entity;

		e.AddPositionListener(this);
		e.AddRotationListener(this);
		e.AddScaleListener(this);
		e.AddDirectionListener(this);

		var pos = e.position.value;
		transform.localPosition = new Vector3(pos.x, pos.y);

		if (isFollowTarget)
			CameraView.followTargets.Add (transform);

	}

	public virtual void OnPosition(LogicEntity entity, FixedVector2 value) {
		position = value.ToVector3 ();
	}

	public virtual void OnRotation(LogicEntity entity, long value) {
		rotation = value.ToFloat ();
	}

	public virtual void OnScale(LogicEntity entity, FixedVector2 value) {

		if (isSquishy) {

			SquashAndStretch s = GetComponent <SquashAndStretch> ();
			if (s == null)
				s = gameObject.AddComponent <SquashAndStretch> ();

			s.ChangeReferenceScale (value.ToVector3 ());
				
		} else {

			transform.localScale = value.ToVector3 ();

		}

	}

	public virtual void OnDirection(LogicEntity entity, int value) {
		if (entity.isWallRiding)
			value = -value;
		sr.flipX = value == -1;
	}

	protected void destroy() {
		Destroy(gameObject);
	}

	public override void Execute (float alpha) {

		transform.localPosition = Vector3.Lerp (
			lastPosition,
			position,
			alpha);

		transform.localRotation = Quaternion.Euler (
			0, 
			0,
			Mathf.Lerp (
				lastRotation,
				rotation,
				alpha
			)
		);
		
	}

	public override void FixedExecute () {

		lastPosition = position;
		transform.position = position;

		lastRotation = rotation;
		transform.localRotation = Quaternion.Euler (
			0, 
			0,
			rotation
		);

	}

	#if UNITY_EDITOR
	public void OnValidate () {
		GetReferences ();
	}

	public virtual void GetReferences () {

		if (anim == null)
			anim = GetComponent <Animator> ();

		if (sr == null)
			sr = GetComponent <SpriteRenderer> ();

	}
	#endif

}
