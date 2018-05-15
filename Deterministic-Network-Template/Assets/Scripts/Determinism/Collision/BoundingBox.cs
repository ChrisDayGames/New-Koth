namespace Determinism {

	public struct BoundingBox {

		public long width;
		public long height;
		public FixedVector2 position;
		public FixedVector2 halfExtents;
		public FixedVector2 size;

		public FixedVector2 min {get {return position - halfExtents;}}
		public FixedVector2 max {get {return position + halfExtents;}}

		public FixedVector2 bottomLeft {get {return min;}}
		public FixedVector2 bottomRight {get {return new FixedVector2 (max.x, min.y);}}
		public FixedVector2 topLeft {get {return new FixedVector2 (min.x, max.y);}}
		public FixedVector2 topRight {get {return max;}}

		public BoundingBox (FixedVector2 _position, FixedVector2 _size) {

			this.width = _size.x;
			this.height = _size.y;

			this.position = _position;
			this.halfExtents = _size / 2;
			this.size = _size;

		}

		public static bool CheckOverlap (BoundingBox a, BoundingBox b) {

			for (int i = 0; i < 2; i++) {

				if (a.min[i] > b.max[i]) return false;
				if (a.max[i] < b.min[i]) return false;

			}

			return true;

		}
			
	}

}