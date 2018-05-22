using System.Linq;

namespace Determinism {

	public class RectilinearCollider : Collider {

		public FixedLine2[] hEdges;
		public FixedLine2[] vEdges;

		public RectilinearCollider (FixedLine2[] _edges) {

			this.edges = _edges;

			int hCount = 0;
			int vCount = 0;

			foreach (FixedLine2 edge in _edges) {

				if (edge.isHorizontal)
					hCount++;

				if (edge.isVertical)
					vCount++;

			}

			hEdges = new FixedLine2[hCount];
			vEdges = new FixedLine2[vCount];

			hCount = 0;
			vCount = 0;

			foreach (FixedLine2 edge in _edges) {

				if (edge.isHorizontal) {
					hEdges[hCount] = edge;
					hCount++;
				}
					

				if (edge.isVertical) {
					vEdges[vCount] = edge;
					vCount++;	
				}
			

			}


		}
			

	}


}