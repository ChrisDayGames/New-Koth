using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Determinism;

namespace ModularUI.Cursors {

	 public class UICollider : UIBehaviour, IUICollider {

		public BoundingBox box;
		public UICollider uiCollider { get { return this as UICollider; } }

		void Awake() {
			Init();
		}

		public virtual void Init() {
			box = new BoundingBox(new FixedVector2(rectTransform.position), new FixedVector2(rectTransform.rect.size));
		}

		void Update () {

			OnUpdate ();

		}

		protected virtual void OnUpdate () {
			
			Resize();
			Move();

		}

		public virtual void Move() {
			
			box.position = new FixedVector2(rectTransform.position);

		}

		public virtual void Resize() {
			
			box.size = new FixedVector2(rectTransform.rect.size);

		}

		public virtual bool IsCollidingWith (UICollider other) { 

			return box.CheckOverlap (other.box);
		
		}

		void OnDrawGizmos() {
			
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(box.position.ToVector3(), box.size.ToVector3());

		}

	}

}