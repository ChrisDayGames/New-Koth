namespace Determinism {

	public class RaycastCollider {

		public readonly long skinWidth = FixedMath.Create (1, 10);
		public readonly long raySpacing = FixedMath.Create (1, 2);

		public int horizontalRayCount;
		public int verticalRayCount;
		public long horizontalRaySpacing;
		public long verticalRaySpacing;

		public Mask check = Mask.DEFAULT;

		public RaycastCollider (BoxCollider collider) {

			CalculateRaySpacing (collider.broadBounds);

		}

		public void CalculateRaySpacing  (BoundingBox box) {

			horizontalRayCount = FixedMath.RoundToInt (box.height.Div(raySpacing));
			verticalRayCount = FixedMath.RoundToInt (box.width.Div(raySpacing));

			horizontalRayCount = (int) FixedMath.Max (horizontalRayCount, 2);
			verticalRayCount = (int) FixedMath.Max (verticalRayCount, 2);

			horizontalRaySpacing = box.height / (horizontalRayCount - 1);
			verticalRaySpacing = box.width / (verticalRayCount - 1);

		}

	}

	public struct RaycastCollisionInfo {

		public Tag left, right, up, down;
		public int leftID, rightID, upID, downID;
		public FixedVector2 horizontalHit, verticalHit;

		public bool horizontalEntered {
			get {
				return Entered (leftID, lastLeftID) || Entered (rightID, lastRightID);
			}
		}

		public bool horizontalExited {
			get {
				return Exited (leftID, lastLeftID) || Exited (rightID, lastRightID);
			}
		}

		public bool verticalEntered {
			get {
				return Entered (upID, lastUpID) || Entered (downID, lastDownID);
			}
		}

		public bool VerticalExited {
			get {
				return Exited (upID, lastUpID) || Exited (downID, lastDownID);
			}
		}

		private int lastLeftID, lastRightID, lastUpID, lastDownID;

		public RaycastCollisionInfo (int _left, int _right, int _up, int _down, FixedVector2 _horizontalHit, FixedVector2 _verticalHit) {

			this.leftID = _left;
			this.rightID = _right;
			this.upID = _up;
			this.downID = _down;

			this.lastLeftID = -1;
			this.lastRightID = -1;
			this.lastUpID = -1;
			this.lastDownID = -1;

			this.horizontalHit = _horizontalHit;
			this.verticalHit = _verticalHit;

			this.left = this.right = this.up = this.down = Tag.NONE;

			this.left = this.GetTagForID (_left);
			this.right = this.GetTagForID (_right);
			this.up = this.GetTagForID (_up);
			this.down = this.GetTagForID (_down);

		}

		public Tag GetTagForID (int _id) {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId (_id);

			if (e == null || !e.hasCollider)
				return Tag.NONE;
			else 
				return e.collider.value.tag;

		}

		public bool CollidesHorizontal () {

			if (this.left != Tag.NONE || this.right != Tag.NONE)
				return true;

			return false;

		}

		public bool CollidesVertical () {

			if (this.down != Tag.NONE || this.up != Tag.NONE)
				return true;

			return false;

		}

		public bool DoesntCollide () {

			if (this.down != Tag.NONE || this.left != Tag.NONE || this.right != Tag.NONE || this.up != Tag.NONE)
				return false;

			return true;

		}

		public bool CollidesWithHorizontal (Tag tag) {

			if (this.left == tag || this.right == tag)
				return true;

			return false;

		}

		public bool CollidesWithVertical (Tag tag) {

			if (this.down == tag || this.up == tag)
				return true;

			return false;

		}

		public bool CollidesWith (Tag tag) {

			if (this.down == tag || this.left == tag || this.right == tag || this.up == tag)
				return true;

			return false;

		}

		public int GetHorizontalEntity (Tag tag) {

			if (this.left == tag)
				return this.leftID;

			if (this.right == tag)
				return this.rightID;

			return -1;

		}

		public int GetVerticalEntity (Tag tag) {

			if (this.up == tag)
				return this.upID;

			if (this.down == tag)
				return this.downID;

			return -1;

		}

		public bool Entered (int current, int last) {
			
			return current != last && current > 0;

		}

		public bool Exited (int current, int last) {

			return last != current && last > 0;

		}

		public void RecordLastPreviousCollision (int left, int right, int up, int down) {

			this.lastLeftID = left;
			this.lastRightID = right;
			this.lastUpID = up;
			this.lastDownID = down;

		}

	}

}


