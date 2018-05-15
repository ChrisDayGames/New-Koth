namespace Determinism {

	public class BoxCollider : Collider {

		public new FixedVector2 position {

			get {return bounds.position;}
			set {bounds.position = value;}

		}

		public long width {

			get {return bounds.width;}
			set {bounds.width = value;}

		}

		public long height {

			get {return bounds.height;}
			set {bounds.height = value;}

		}

		public BoxCollider (FixedVector2 position, FixedVector2 halfExtents) {
			bounds = new BoundingBox (position, halfExtents);

			edges = new FixedLine2[] {

				new FixedLine2 (
					bounds.min,
					new FixedVector2 (bounds.max.x, bounds.min.y)
				),

				new FixedLine2 (
					new FixedVector2 (bounds.min.x, bounds.max.y),
					bounds.max
				),

				new FixedLine2 (
					bounds.min,
					new FixedVector2 (bounds.min.x, bounds.max.y)
				),
					
				new FixedLine2 (
					new FixedVector2 (bounds.max.x, bounds.min.y),
					bounds.max
				)


			};
		}

		public BoxCollider (FixedVector2 _position, FixedVector2 _positionOffset, FixedVector2 _size) {

			positionOffset = _positionOffset;
			bounds = new BoundingBox (_position + positionOffset, _size);

			edges = new FixedLine2[] {

				new FixedLine2 (
					bounds.min,
					new FixedVector2 (bounds.max.x, bounds.min.y)
				),

				new FixedLine2 (
					new FixedVector2 (bounds.min.x, bounds.max.y),
					bounds.max
				),

				new FixedLine2 (
					bounds.min,
					new FixedVector2 (bounds.min.x, bounds.max.y)
				),

				new FixedLine2 (
					new FixedVector2 (bounds.max.x, bounds.min.y),
					bounds.max
				)

			};

		}

		public override void MoveCollider (FixedVector2 newPosition) {

			position = newPosition + positionOffset;
			UpdateEdges ();
			
		}

		public override void ScaleCollider (FixedVector2 newScale) {}

		public void UpdateEdges () {

			edges = new FixedLine2[] {

				new FixedLine2 (
					bounds.min,
					new FixedVector2 (bounds.max.x, bounds.min.y)
				),

				new FixedLine2 (
					new FixedVector2 (bounds.min.x, bounds.max.y),
					bounds.max
				),

				new FixedLine2 (
					bounds.min,
					new FixedVector2 (bounds.min.x, bounds.max.y)
				),

				new FixedLine2 (
					new FixedVector2 (bounds.max.x, bounds.min.y),
					bounds.max
				)

			};

		}

	}

}
