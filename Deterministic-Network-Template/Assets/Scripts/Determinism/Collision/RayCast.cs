using Determinism;
using Entitas;

public static class RayCast {

	public static bool CheckIntersection (FixedVector2 origin, FixedVector2 length, out FixedVector2 intersection, Determinism.Collider collider) {

		if (collider is BoxCollider) return CheckIntersection (origin, length, out intersection, (BoxCollider) collider);
		if (collider is RectilinearCollider) return CheckIntersection (origin, length, out intersection, (RectilinearCollider) collider);
		if (collider is PolygonCollider) return CheckIntersection (origin, length, out intersection, (PolygonCollider) collider);

		intersection = FixedVector2.NAN;
		return false;

	}

	public static bool CheckIntersection (FixedVector2 origin, FixedVector2 length, out FixedVector2 intersection, Determinism.BoxCollider collider) {

		intersection = FixedVector2.NAN;
		FixedLine2 l = new FixedLine2 (origin, origin + length);

		if (l.isHorizontal)
			for (int i = 2; i < 4; i++) {

//				if (FixedMath.Min (l.start.y, l.end.y) > collider.edges[i].start.y) continue;
//				if (FixedMath.Max (l.start.y, l.end.y) < collider.edges[i].start.y) continue;

//				if (FixedLine2.LineIntersection (l, collider.edges[i], out intersection))
//					return true;

				if (FixedLine2.AAOrthogonalLineIntersection (l, collider.edges[i], out intersection))
					return true;

			}

		if (l.isVertical)
			for (int i = 0; i < 2; i++){

				//if (edge.isVertical) continue;
//				if (FixedMath.Min (l.start.x, l.end.x) > collider.edges[i].start.x) continue;
//				if (FixedMath.Max (l.start.x, l.end.x) < collider.edges[i].start.x) continue;

//				if (FixedLine2.LineIntersection (l, collider.edges[i], out intersection))
//					return true;

				if (FixedLine2.AAOrthogonalLineIntersection (collider.edges[i], l, out intersection))
					return true;

			}

		return false;

		intersection = FixedVector2.NAN;
		return FixedVector2.LineBoxIntersection (collider.broadBounds, origin, length, out intersection);

	}

	public static bool CheckIntersection (FixedVector2 origin, FixedVector2 length, out FixedVector2 intersection, PolygonCollider collider) {

		intersection = FixedVector2.NAN;
		FixedLine2 l = new FixedLine2 (origin, origin + length);

		foreach (FixedLine2 edge in collider.edges) {

			//if (edge.isVertical) continue;
			//if (FixedMath.Min (l.start.y, l.end.y) > edge.start.y) continue;
			//if (FixedMath.Max (l.start.y, l.end.y) < edge.start.y) continue;
	
			if (FixedLine2.LineIntersection (l, edge, out intersection)) {
				return true;
			}

		}

		return false;

	}

	public static bool CheckIntersection (FixedVector2 origin, FixedVector2 length, out FixedVector2 intersection, RectilinearCollider collider) {

		intersection = FixedVector2.NAN;
		FixedLine2 l = new FixedLine2 (origin, origin + length);

		if (l.isHorizontal)
			foreach (FixedLine2 edge in collider.vEdges) {

				//if (FixedMath.Min (l.start.y, l.end.y) > edge.start.y) continue;
				//if (FixedMath.Max (l.start.y, l.end.y) < edge.start.y) continue;
				if (FixedLine2.AAOrthogonalLineIntersection (l, edge, out intersection))
					return true;
//
//				if (FixedLine2.LineIntersection (l, edge, out intersection)) {
//					return true;
//				}

			}

		if (l.isVertical)
			foreach (FixedLine2 edge in collider.hEdges) {

				//if (edge.isVertical) continue;
				//if (FixedMath.Min (l.start.x, l.end.x) > edge.start.x) continue;
				//if (FixedMath.Max (l.start.x, l.end.x) < edge.start.x) continue;

				if (FixedLine2.AAOrthogonalLineIntersection (edge, l, out intersection))
					return true;

//				if (FixedLine2.LineIntersection (l, edge, out intersection)) {
//					return true;
//				}

			}

		return false;

	}

}
