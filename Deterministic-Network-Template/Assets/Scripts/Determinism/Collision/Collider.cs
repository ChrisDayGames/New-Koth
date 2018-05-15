using System.Linq;


namespace Determinism {

	public abstract class Collider {


		public Mask mask;
		public Tag tag;

		public Mask check;

		protected BoundingBox bounds;
		protected FixedVector2 positionOffset;

		public FixedLine2[] edges;

		public BoundingBox broadBounds {get{return bounds;}}
		public FixedVector2 position {get{return bounds.position;} set {bounds.position = value;}}
		public FixedVector2 halfExtents {get{return bounds.halfExtents;}}
		public bool isTrigger = false;

		public virtual void MoveCollider (FixedVector2 newPosition) {}

		public virtual void ScaleCollider (FixedVector2 newScale) {}

		public static bool CheckOverlapBroad (Collider a, Collider b) {

			return BoundingBox.CheckOverlap (a.broadBounds, b.broadBounds);

		}

		public static bool CheckOverlapNarrow (BoxCollider a, BoxCollider b) {

			return BoundingBox.CheckOverlap (a.broadBounds, b.broadBounds);

		}

		public static bool CheckOverlapNarrow (OvalCollider a, OvalCollider b) {

			return BoundingBox.CheckOverlap (a.broadBounds, b.broadBounds);

		}

		public static bool CheckOverlapNarrow (OvalCollider a, BoxCollider b){
			
			return CheckOverlapNarrow (b, a);

		}

		public static bool CheckOverlapNarrow (BoxCollider a, OvalCollider b){

			return false;

		}

	}
		
}

