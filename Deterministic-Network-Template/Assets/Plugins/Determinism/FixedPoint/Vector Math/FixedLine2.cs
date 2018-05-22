using Entitas;
using UnityEngine;

namespace Determinism {
	[System.Serializable]
	public class FixedLine2 {

		public FixedVector2 start, end;
		public bool isHorizontal, isVertical;

		private long m, b;

		public long slope {

			get {
				return m;
			}

		}

		public long yIntercept {

			get {
				return b;
			}

		}

		public FixedLine2 () {

			this.start = FixedVector2.ZERO;
			this.end = FixedVector2.ZERO;

		}

		public FixedLine2 (FixedVector2 _start, FixedVector2 _end) {

			this.start = _start;
			this.end = _end;

			isHorizontal = start.y == end.y;
			isVertical = start.x == end.x;

			this.m = 1;
			this.b = 1;

			//slope of y = m * x + b line form
			//this.m = (end.y - start.y).Div(end.x - start.x);

			//yIntercept of y = m * x + b line form
			//this.b = start.y - slope.Mul(start.x);
		}

		public void UpdateSlope () {

			isHorizontal = start.y == end.y;
			isVertical = start.x == end.x;

			this.m = 1;

			//this.m = (end.y - start.y).Div(end.x - start.x);

		}

		public void UpdateYIntercept () {

			this.b = start.y - slope.Mul(start.x);

		}

		public static bool AreParallel (FixedLine2 a, FixedLine2 b) {

			return a.slope == b.slope;

		}

		public static bool IntersectionExists (FixedLine2 a, FixedLine2 b) {

			int sign1 = (a.start.y - (b.slope.Mul(a.start.x)) - b.yIntercept).Sign ();
			int sign2 = (a.end.y - (b.slope.Mul(a.end.x)) - b.yIntercept).Sign ();

			return sign1 != sign2;

		}

		public static bool LineLineIntersection (FixedLine2 line1, FixedLine2 line2, out FixedVector2 intersection) {
			
			intersection = new FixedVector2 (long.MinValue, long.MinValue);

			//standard form of line 1
			long a1 = line1.end.y - line1.start.y;
			long b1 = line1.start.x - line1.end.x;
			long c1 = a1.Mul(line1.start.x) - b1.Mul(line1.start.y);

			//standard form of line 2
			long a2 = line2.end.y - line2.start.y;
			long b2 = line2.start.x - line2.end.x;
			long c2 = a2.Mul(line2.start.x) - b2.Mul(line2.start.y);

	//		float delta = A1*B2 - A2*B1;
	//		if(delta == 0) 
	//			throw new ArgumentException("Lines are parallel");
	//
	//		float x = (B2*C1 - B1*C2)/delta;
	//		float y = (A1*C2 - A2*C1)/delta;

			long determinant = a1.Mul (b2) - a2.Mul (b1);

			//line are parallel
			if (determinant.Abs () < FixedMath.Hundredth)
				return false;

			long x = (b2.Mul(c1) - b1.Mul(c2)).Div (determinant);
			long y = (a1.Mul(c2) - a2.Mul(c1)).Div (determinant);

			intersection = new FixedVector2 (x, y);

	//		UnityEngine.Debug.DrawLine (new FixedVector2 (0, 0).ToVector3 (), intersection.ToVector3 ());

			if ((FixedMath.Min (line1.start.x, line1.end.x) < x && x < FixedMath.Max (line1.start.x, line1.end.x))
				&& (FixedMath.Min (line1.start.y, line1.end.y) < y && y < FixedMath.Max (line1.start.y, line1.end.y)))
				return true;

			return false;

		}

		private static FixedLine2 horizontal, vertical;
		public static bool LineIntersection (FixedLine2 left, FixedLine2 right, out FixedVector2 intersection) {

			intersection = FixedVector2.NAN;

			//check if we have parralel lines (even if we have two overlapping lines)
			if ((left.isHorizontal && right.isHorizontal) || ((left.isVertical && right.isVertical)))
				return false;

			if (left.isHorizontal && right.isVertical) {
				horizontal = left;
				vertical = right;
			}

			else if (right.isHorizontal && left.isVertical) {
				horizontal = right;
				vertical = left;
			}

			intersection = new FixedVector2 (vertical.start.x, horizontal.start.y);

			if ((FixedMath.Min(horizontal.start.x, horizontal.end.x) <= intersection.x) && intersection.x <= FixedMath.Max(horizontal.start.x, horizontal.end.x)
				&& (FixedMath.Min(vertical.start.y, vertical.end.y) <= intersection.y) && intersection.y <= FixedMath.Max(vertical.start.y, vertical.end.y))
				return true; 


			//check for sloped lines while accounting for vertical lines
			return false;

		}

		public static bool LineIntersection (FixedVector2 startA, FixedVector2 endA, FixedVector2 startB, FixedVector2 endB, out FixedVector2 intersection) {

			intersection = FixedVector2.NAN;

			bool aIsHorizontal, aIsVertical, bIsHorizontal, bIsVertical;
			aIsHorizontal = aIsVertical = bIsHorizontal = bIsVertical = false;


			if (startA.y == endA.y)
				aIsHorizontal = true;
			if (startA.x == endA.x)
				aIsVertical = true;

			if (startB.y == endB.y)
				bIsHorizontal = true;
			if (startB.x == endB.x)
				bIsVertical = true;


			//check if we have parralel lines (even if we have two overlapping lines)
			if ((aIsHorizontal && bIsHorizontal) || ((aIsVertical && bIsVertical)))
				return false;

			if (aIsHorizontal && bIsVertical)
				intersection = new FixedVector2 (startB.x, startA.y);

			else if (bIsHorizontal && aIsVertical)
				intersection = new FixedVector2 (startA.x, startB.y);

			if (aIsHorizontal 
				&& (FixedMath.Min(startA.x, endA.x) < intersection.x) && intersection.x < FixedMath.Max(startA.x, endA.x)
				&& (FixedMath.Min(startB.y, endB.y) < intersection.y) && intersection.y < FixedMath.Max(startB.y, endB.y))
					return true; 
			
			if (bIsHorizontal
				&& (FixedMath.Min(startB.x, endB.x) < intersection.x) && intersection.x < FixedMath.Max(startB.x, endB.x)
				&& (FixedMath.Min(startA.y, endA.y) < intersection.y) && intersection.y < FixedMath.Max(startA.y, endA.y))
				return true; 
			

			//check for sloped lines while accounting for vertical lines
			return false;

		}


		public static bool AAOrthogonalLineIntersection (FixedLine2 horizontal, FixedLine2 vertical, out FixedVector2 intersection) {

			intersection = new FixedVector2 (vertical.start.x, horizontal.start.y);

			if ((FixedMath.Min(horizontal.start.x, horizontal.end.x) < intersection.x) && intersection.x < FixedMath.Max(horizontal.start.x, horizontal.end.x)
				&& (FixedMath.Min(vertical.start.y, vertical.end.y) < intersection.y) && intersection.y < FixedMath.Max(vertical.start.y, vertical.end.y))
				return true; 


			//check for sloped lines while accounting for vertical lines
			return false;


		}

	//	public static bool LineLineIntersection (FixedLine2 a, FixedLine2 b, out FixedVector2 intersection) {
	//
	//		intersection = new FixedVector2 (long.MinValue, long.MinValue);
	//
	//		if (AreParallel (a, b))
	//			return false;
	//
	//		if (!IntersectionExists (a, b) || !IntersectionExists (b, a))
	//			return false;
	//		
	//		long x = (b.yIntercept - a.yIntercept).Div (a.slope - b.slope);
	//		long y = a.slope.Mul (x) + a.yIntercept;
	//
	//		intersection = new FixedVector2 (x, y);
	//
	//		return true;
	//
	//	}
			
	}

}