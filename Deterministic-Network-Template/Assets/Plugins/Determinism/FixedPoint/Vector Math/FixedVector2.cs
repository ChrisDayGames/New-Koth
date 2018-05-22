using UnityEngine;
using UnityEngine.Serialization;
using System;

namespace Determinism {

	[Serializable]
	public struct FixedVector2 {

		#region Constansts

		public readonly static FixedVector2 NAN =  new FixedVector2 (FixedMath.NAN, FixedMath.NAN);
		public readonly static FixedVector2 ZERO =  new FixedVector2 ();
		public readonly static FixedVector2 HALF =  new FixedVector2 (FixedMath.HALF, FixedMath.HALF);
		public readonly static FixedVector2 ONE =  new FixedVector2 (FixedMath.ONE, FixedMath.ONE);

		public readonly static FixedVector2 RIGHT =  new FixedVector2 (FixedMath.ONE, 0);
		public readonly static FixedVector2 LEFT =  new FixedVector2 (-FixedMath.ONE, 0);
		public readonly static FixedVector2 UP =  new FixedVector2 (0, FixedMath.ONE);
		public readonly static FixedVector2 DOWN =  new FixedVector2 (0, -FixedMath.ONE);

		public static FixedVector2 calculator = ZERO;
		public static Vector2 calculatorV2 = Vector3.zero;
		public static Vector3 calculatorV3 = Vector3.zero;

		#endregion

		#region Constructors

		[SerializeField, FormerlySerializedAs("x")]
		private long rawX;

		[SerializeField, FormerlySerializedAs("y")]
		private long rawY;

		public long x {get{return rawX;} set{rawX = value;}}
		public long y {get{return rawY;} set{rawY = value;}}

		public long this[int index] {

			get {
				return GetDimension (index);
			}

			set {
				SetDimension (index, value);
			}

		}

		public FixedVector2 (long _x = 0, long _y = 0) {

			this.rawX = _x;
			this.rawY = _y;

		}

		public FixedVector2 (int _x, int _y) {

			this.rawX = FixedMath.Create (_x);
			this.rawY = FixedMath.Create (_y);

		}

		public FixedVector2 (float _x, float _y) {

			this.rawX = FixedMath.Create (_x);
			this.rawY = FixedMath.Create (_y);

		}

		public FixedVector2 (double _x, double _y) {

			this.rawX = FixedMath.Create (_x);
			this.rawY = FixedMath.Create (_y);

		}

		public FixedVector2 (Vector2 vector) {

			this.rawX = FixedMath.Create (vector.x);
			this.rawY = FixedMath.Create (vector.y);

		}

		public FixedVector2 (Vector3 vector) {

			this.rawX = FixedMath.Create (vector.x);
			this.rawY = FixedMath.Create (vector.y);

		}

		#endregion

		#region Casts

		public Vector2 ToVector2 () {

			calculatorV2.x = FixedMath.ToFloat (this.x); 
			calculatorV2.y = FixedMath.ToFloat (this.y);

			return calculatorV2;

		}

		public Vector3 ToVector3 () {

			calculatorV3.x = FixedMath.ToFloat (this.x); 
			calculatorV3.y = FixedMath.ToFloat (this.y);
			calculatorV3.z = 0;

			return calculatorV3;

		}

		#endregion

		#region Operators

		public static FixedVector2 operator + (FixedVector2 a, FixedVector2 b) {

			a.x += b.x;
			a.y += b.y;

			return a;

		}

		public static FixedVector2 operator - (FixedVector2 a, FixedVector2 b) {

			a.x -= b.x;
			a.y -= b.y;

			return a;

		}

		public static FixedVector2 operator * (FixedVector2 a, int s) {

			a.x = (a.x * s);
			a.y = (a.y * s);

			return a;

		}

		public static FixedVector2 operator * (FixedVector2 a, long s) {

			a.x = (a.x * s) >> FixedMath.DECIMAL_BITS;
			a.y = (a.y * s) >> FixedMath.DECIMAL_BITS;

			return a;

		}

		public static FixedVector2 operator / (FixedVector2 a, int s) {

			a.x = a.x / s;
			a.y = a.y / s;

			return a;

		}

		public static FixedVector2 operator / (FixedVector2 a, long s) {

			a.x = (a.x << FixedMath.DECIMAL_BITS) / s;
			a.y = (a.y << FixedMath.DECIMAL_BITS) / s;

			return a;

		}

		public static bool operator == (FixedVector2 a, FixedVector2 b) {
	
			return a.x == b.x && a.y == b.y;

		}

		public static bool operator != (FixedVector2 a, FixedVector2 b) {

			return a.x != b.x || a.y != b.y;

		}

		public override bool Equals (object obj) {
			
			return base.Equals (obj);

		}

		public override int GetHashCode () {
			
			return base.GetHashCode ();

		}

		#endregion

		#region General Math

		public long squareMagnitude {

			get{
				return FixedVector2.SquareDistance (FixedVector2.ZERO, this);
			}

		}

		public long magnitude {

			get{
				return FixedVector2.Distance (FixedVector2.ZERO, this);
			}

		}

		public FixedVector2 normalized {

			get{

				long magnitude = this.magnitude;

				if (magnitude == 0) return FixedVector2.ZERO;

				calculator = this;
				calculator.x = (calculator.x << FixedMath.DECIMAL_BITS) / magnitude;
				calculator.y = (calculator.y << FixedMath.DECIMAL_BITS) / magnitude;

				return calculator;

			}

		}

		public static long SquareDistance (FixedVector2 from, FixedVector2 to) {

			long deltaX = (to.x - from.x);
			long deltaY = (to.y - from.y);

			return 
				((deltaX * deltaX) >> FixedMath.DECIMAL_BITS)
				+ ((deltaY * deltaY) >> FixedMath.DECIMAL_BITS);

		}

		public static long Distance (FixedVector2 from, FixedVector2 to) {

			long deltaX = (to.x - from.x);
			long deltaY = (to.y - from.y);

			return FixedMath.Sqrt (

				((deltaX * deltaX) >> FixedMath.DECIMAL_BITS)
				+ ((deltaY * deltaY) >> FixedMath.DECIMAL_BITS)

			);

		}

		public void Normalize () {

			this = this.normalized;

		}

		public static FixedVector2 Lerp (FixedVector2 start, FixedVector2 end, long t) {

			calculator.x = (((end.x - start.x) * t) >> FixedMath.DECIMAL_BITS) + start.x;
			calculator.y = (((end.y - start.y) * t) >> FixedMath.DECIMAL_BITS) + start.y;

			return calculator;

		}

		public FixedVector2 SetX (long _x) {

			this.x = _x;
			return this;

		}

		public FixedVector2 SetY (long _y) {

			this.y = _y;
			return this;

		}

		public FixedVector2 FlipX () {

			return new FixedVector2 (-this.x, this.y);

		}

		public FixedVector2 FlipY () {

			return new FixedVector2 (this.x, -this.y);

		}

		public FixedVector2 Reverse () {

			return new FixedVector2 (-this.x, -this.y);

		}


		#endregion

		#region Raycasting

		public static bool ClipLine (int d, BoundingBox box, FixedVector2 origin, FixedVector2 end, ref long lowFraction, ref long highFraction) {

			long lowDimensionFraction, highDimensionFraction;
			long delta = (end[d] - origin[d]);

			if (delta != 0) {

				lowDimensionFraction = (box.min[d] - origin[d]).Div (delta);
				highDimensionFraction = (box.max[d] - origin[d]).Div (delta);

			} else {

				return box.min[d] <= end[d] && end[d] <= box.max[d];

			}

			if (highDimensionFraction < lowDimensionFraction)
				FixedMath.Swap (ref lowDimensionFraction, ref highDimensionFraction);

			if (highDimensionFraction < lowFraction)
				return false;

			if (lowDimensionFraction > highFraction)
				return false;

			lowFraction = FixedMath.Max (lowDimensionFraction, lowFraction);
			highFraction = FixedMath.Min (highDimensionFraction, highFraction);

			if (lowFraction > highFraction)
				return false;

			return true;

		}

		public static bool LineBoxIntersection (BoundingBox box, FixedVector2 origin, FixedVector2 length, out FixedVector2 intersection) {

			FixedVector2 end = origin + length;
			intersection = FixedVector2.NAN;

			long lowFraction = 0;
			long highFraction = FixedMath.ONE;

			for (int d = 0; d < 2; d++) {

				if (!ClipLine (d, box, origin, end, ref lowFraction, ref highFraction))
					return false;

			}

			intersection = origin + (length * lowFraction);

			return true;

		}

		public static bool LineBoxIntersection (int d, BoundingBox box, FixedVector2 origin, FixedVector2 length, out FixedVector2 intersection) {

			FixedVector2 end = origin + length;
			intersection = FixedVector2.NAN;

			long lowFraction = 0;
			long highFraction = FixedMath.ONE;

			if (!ClipLine (d, box, origin, end, ref lowFraction, ref highFraction))
				return false;

			intersection = origin + (length * lowFraction);

			return true;

		}


		#endregion

		#region Getter

		public long GetDimension (int index) {

			switch (index) {

			case 0: 
				return this.x;

			case 1: 
				return this.y;

			default:
				Debug.LogError ("Dimension does not exist.  Out of bounds");
				break;
			}

			return 0;

		}

		public void SetDimension (int index, long value) {

			switch (index) {

			case 0: 
				this.x = value;
				break;

			case 1: 
				this.y = value;
				break;

			default:
				Debug.LogError ("Dimension does not exist.  Out of bounds");
				break;
			}

		}

		#endregion

		public override string ToString ()
		{
			return "x: " + this.x + " | y: " + this.y;
		}

	}

}
