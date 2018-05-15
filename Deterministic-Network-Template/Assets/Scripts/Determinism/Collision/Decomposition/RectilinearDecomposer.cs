//this script is based on this research paper
//http://eprints.cs.vt.edu/archive/00000102/01/TR-88-17.pdf

using Determinism;
using Entitas;
using System.Collections.Generic;
using System.Linq;

public static class RectilinearDecomposer {

	public static List <Vertex> drawVertices;
	public static List <Edge> drawEdgeHorizontal;
	public static List <Edge> drawEdgeVertical;
	public static List <Vertex> drawContour;
	public static List<List<Vertex>> drawHoles;

	public static List <Edge> drawEdgeCollinearHorizontal;
	public static List <Edge> drawEdgeCollinearVertical;


	public static FixedLine2[] BreakIntoEdges (List <FixedVector2> points) {

		//Step 1:
		//The list of vertices to be manipulate in this decomp
		//
		//
		//NOTE: These are not vectors, they are special classes that store data for rectilinear decomp
		List <Vertex> hVertices = new List<Vertex> ();

		//Step 2:
		//The vector 2s are converted into Rectilinear vertices and stored
		foreach (FixedVector2 point in points) {
			hVertices.Add (new Vertex (point));
		}

		//Step 3:
		//get number of verteces
		int numberOfVerteces = hVertices.Count;

		//Step 4:
		//sort vertices so that they meet the condition

		//i) yi < yj
		//or
		//ii) yi = yj && xi < xj
		hVertices = hVertices.OrderBy (o => o.position.x).ToList (); //System.Linq -> Lambda wow so cool
		hVertices = hVertices.OrderBy (o => o.position.y).ToList (); //System.Linq -> Lambda wow so cool

		//Step 5:
		//create a list to store the edges
		List <Edge> horizontalEdges = new List <Edge> ();
		List <Edge> verticalEdges = new List <Edge> ();

		//Step 6:
		//find neighbours of each vertex
		//create edges
		for (int i = 0; i < hVertices.Count - 1; i++) {

			if (hVertices[i].horizontalNeighbour == null) {

				//become horizontal neighbours
				hVertices[i].BecomeHorizontalNeighbours(hVertices[i+1]);
				horizontalEdges.Add (new Edge (hVertices [i], hVertices[i+1]));

			} 

			if (hVertices[i].verticalNeighbour == null) {

				for (int j = i + 1; j < hVertices.Count; j++) {

					if (i != j && hVertices[i].position.x == hVertices[j].position.x) {

						//become vertical neighbours
						hVertices[i].BecomeVerticalNeighbours(hVertices[j]);
						verticalEdges.Add (new Edge (hVertices [i], hVertices[j]));

						break;

					}

				}

			}

		}

		List <FixedLine2> allEdges = new List<FixedLine2> ();

		//sort hEdges
		horizontalEdges = horizontalEdges.OrderBy (o => o.start.x).ToList (); //System.Linq -> Lambda wow so cool
		horizontalEdges = horizontalEdges.OrderBy (o => o.start.y).ToList (); //System.Linq -> Lambda wow so cool

		int startIndex, endIndex;
		startIndex = endIndex = 0;
		for (int i = 0; i < horizontalEdges.Count - 1; i++) {

			if (horizontalEdges[i].end == horizontalEdges[i+1].start) {

				//end is the next line
				endIndex = i+1;

			} else {

				//add the edges
				if (horizontalEdges[startIndex].start != horizontalEdges[endIndex].end)
					allEdges.Add(new FixedLine2 (horizontalEdges[startIndex].start, horizontalEdges[endIndex].end));

				//set indeces to the next edge
				startIndex = i+1;
				endIndex = i+1;

			}

		}

		//sort vEdges
		verticalEdges = verticalEdges.OrderBy (o => o.start.y).ToList (); //System.Linq -> Lambda wow so cool
		verticalEdges = verticalEdges.OrderBy (o => o.start.x).ToList (); //System.Linq -> Lambda wow so cool

		startIndex = endIndex = 0;
		for (int i = 0; i < verticalEdges.Count - 1; i++) {

			if (verticalEdges[i].end == verticalEdges[i+1].start) {

				//end is the next line
				endIndex = i+1;

			} else {

				//add the edges
				if (verticalEdges[startIndex].start != verticalEdges[endIndex].end)
					allEdges.Add(new FixedLine2 (verticalEdges[startIndex].start, verticalEdges[endIndex].end));

				//set indeces to the next edge
				startIndex = i+1;
				endIndex = i+1;

			}

		}

//		FixedLine2[] allEdges = new FixedLine2[horizontalEdges.Count + verticalEdges.Count];
//
//		for (int i = 0; i < horizontalEdges.Count; i++) {
//
//			allEdges[i] = new FixedLine2 (horizontalEdges[i].start, horizontalEdges[i].end);
//
//		}
//
//
//		int offset = horizontalEdges.Count;
//		for (int i = 0; i < verticalEdges.Count; i++) {
//
//			allEdges[i + offset] = new FixedLine2 (verticalEdges[i].start, verticalEdges[i].end);
//
//		}


		return allEdges.ToArray ();

	}

	public static BoxCollider[] BreakIntoRectangles (List <FixedVector2> points) {



		//Step 1:
		//The list of vertices to be manipulate in this decomp
		//
		//
		//NOTE: These are not vectors, they are special classes that store data for rectilinear decomp
		List <Vertex> hVertices = new List<Vertex> ();

		//Step 2:
		//The vector 2s are converted into Rectilinear vertices and stored
		foreach (FixedVector2 point in points) {
			hVertices.Add (new Vertex (point));
		}

		//Step 3:
		//get number of verteces
		int numberOfVerteces = hVertices.Count;

		//Step 4:
		//sort vertices so that they meet the condition

		//i) yi < yj
		//or
		//ii) yi = yj && xi < xj
		hVertices = hVertices.OrderBy (o => o.position.x).ToList (); //System.Linq -> Lambda wow so cool
		hVertices = hVertices.OrderBy (o => o.position.y).ToList (); //System.Linq -> Lambda wow so cool

		drawVertices = hVertices;

		//Step 5:
		//create a list to store the edges
		List <Edge> horizontalEdges = new List <Edge> ();
		List <Edge> verticalEdges = new List <Edge> ();

		//Step 6:
		//find neighbours of each vertex
		//create edges
		for (int i = 0; i < hVertices.Count - 1; i++) {

			if (hVertices[i].horizontalNeighbour == null) {

				//become horizontal neighbours
				hVertices[i].BecomeHorizontalNeighbours(hVertices[i+1]);
				horizontalEdges.Add (new Edge (hVertices [i], hVertices[i+1]));

			} 

			if (hVertices[i].verticalNeighbour == null) {

				for (int j = i + 1; j < hVertices.Count; j++) {
					
					if (i != j && hVertices[i].position.x == hVertices[j].position.x) {

						//become vertical neighbours
						hVertices[i].BecomeVerticalNeighbours(hVertices[j]);
						verticalEdges.Add (new Edge (hVertices [i], hVertices[j]));

						break;

					}
					
				}

			}

		}
			
		drawEdgeHorizontal = horizontalEdges;
		drawEdgeVertical = verticalEdges;

		//Step 7:
		//Order the vertices in the contour of the shape
		List <Vertex> contour = new List <Vertex> ();

		hVertices[0].visited = true;
		contour.Add (hVertices[0]);

		hVertices[1].visited = true;
		contour.Add (hVertices[1]);


		int last = contour.Count - 1;
		while (!contour[last].horizontalNeighbour.visited || !contour[last].verticalNeighbour.visited) {

			if (!contour[last].horizontalNeighbour.visited) {
				contour[last].horizontalNeighbour.visited = true;
				contour.Add (contour[last].horizontalNeighbour);
			}

			else if (!contour[last].verticalNeighbour.visited) {
				contour[last].verticalNeighbour.visited = true;
				contour.Add (contour[last].verticalNeighbour);
			}

			last = contour.Count - 1;

		}

		drawContour = contour;

		//Step 8:
		//Order the vertices in the contour of the holes
		List<List<Vertex>> holes = new List<List<Vertex>> ();

		for (int i = 0; i < hVertices.Count; i++) {

			if (!hVertices[i].visited) {

				holes.Add (new List<Vertex> ());
				int holeIndex = holes.Count - 1;

				holes[holeIndex].Add (hVertices[i]);
				hVertices[i].visited = true;
				last = holes[holeIndex].Count - 1;

				while (!holes[holeIndex][last].horizontalNeighbour.visited || !holes[holeIndex][last].verticalNeighbour.visited) {

					if (!holes[holeIndex][last].horizontalNeighbour.visited) {
						holes[holeIndex][last].horizontalNeighbour.visited = true;
						holes[holeIndex].Add (holes[holeIndex][last].horizontalNeighbour);

					} 

					else if (!holes[holeIndex][last].verticalNeighbour.visited) {
						holes[holeIndex][last].verticalNeighbour.visited = true;
						holes[holeIndex].Add (holes[holeIndex][last].verticalNeighbour);
					}

					last = holes[holeIndex].Count - 1;

				}

			}

		}


		drawHoles = holes;

		//Step 9:
		//Decide whether or not the contour verteces are convex
		Direction lastDirection = Direction.WEST;
		Vertex v = contour[0];

		while (!v.hasCheckConcavity) {

			v.isConcave = !v.CheckIfConcave (lastDirection);
			lastDirection = v.direction;

			if (lastDirection == Direction.EAST || lastDirection == Direction.WEST)
				v = v.horizontalNeighbour;
			
			else
				v = v.verticalNeighbour;
			
				
		}
			
		int holeNumber = 0;
		//Step 10: 
		//Decide whether or not the hole verteces are convex
		foreach (List<Vertex> hole in holes) {

			lastDirection = Direction.WEST;
			v = hole[0];

			while (!v.hasCheckConcavity) {

				v.isConcave = v.CheckIfConcave (lastDirection);

				if (holeNumber > 0)
					v.isConcave = !v.isConcave;
					
				lastDirection = v.direction;

				if (lastDirection == Direction.EAST || lastDirection == Direction.WEST)
					v = v.horizontalNeighbour;

				else
					v = v.verticalNeighbour;

			}

			holeNumber++;

		}
			
		//Step 11:
		//Create a new list of vertices to sort vertically
		List <Vertex> vVertices = hVertices;

		//Step 12:
		//sort vertices so that they meet the condition

		//i) xi < xj
		//or
		//ii) xi = xj && yi < yj
		vVertices = vVertices.OrderBy (o => o.position.y).ToList (); //System.Linq -> Lambda wow so cool
		vVertices = vVertices.OrderBy (o => o.position.x).ToList (); //System.Linq -> Lambda wow so cool

		//Step 13:
		//Create collinear edge arrays
		List <Edge> horizontalCollinearEdges = new List <Edge> ();
		List <Edge> verticalCollinearEdges = new List <Edge> ();


		//Step 14:
		//Find Colinear edgdes
		for (int i = 0; i < hVertices.Count - 3; i++) {

			if (hVertices[i].position.y == hVertices[i+1].position.y
				&& hVertices[i+1].position.y == hVertices[i+2].position.y
				&& hVertices[i+2].position.y == hVertices[i+3].position.y
				&& hVertices[i+1].isConcave
				&& hVertices[i+2].isConcave) {

				bool crossesEdge = false;

				foreach (Edge e  in verticalEdges) {

					if (hVertices[i+1].position.x < e.start.x && e.start.x < hVertices[i+2].position.x
						&& e.start.y < hVertices[i+1].position.y && hVertices[i+1].position.y < e.end.y) {

						crossesEdge = true;
						continue;
						  
					}
					
				}

				if (crossesEdge)
					continue;

				horizontalCollinearEdges.Add (new Edge (
					hVertices[i+1].position, 
					hVertices[i+2].position));

			}

		}
			
		//Step 15:
		//Find Colinear edgdes
		for (int i = 0; i < vVertices.Count - 3; i++) {

			if (vVertices[i].position.x == vVertices[i+1].position.x
				&& vVertices[i+1].position.x == vVertices[i+2].position.x
				&& vVertices[i+2].position.x == vVertices[i+3].position.x
				&& vVertices[i+1].isConcave
				&& vVertices[i+2].isConcave) {

				bool crossesEdge = false;

				foreach (Edge e  in horizontalEdges) {

					if (vVertices[i+1].position.y < e.start.y && e.start.y < vVertices[i+2].position.y
						&& e.start.x < vVertices[i+1].position.x && vVertices[i+1].position.x < e.end.x) {

						crossesEdge = true;
						break;

					}

				}

				if (crossesEdge)
					continue;

				verticalCollinearEdges.Add (new Edge (
					vVertices[i+1].position, 
					vVertices[i+2].position));


			}

		}

		//Step 16: merge the new edges with the old edges
		//
		//horizontalCollinearEdges.AddRange (horizontalEdges);
		//verticalCollinearEdges.AddRange (verticalEdges);

		//Step 17: merge collinear edges on the horizontal
		//
		horizontalCollinearEdges = horizontalCollinearEdges.OrderBy (e => e.start.x).ToList ();
		horizontalCollinearEdges = horizontalCollinearEdges.OrderBy (e => e.start.y).ToList ();


		List<Edge> tempHorizontalEdges =  new List <Edge> ();

		for (int i = 0; i < horizontalCollinearEdges.Count; i++) {

			//if there is anything in the list
			if (tempHorizontalEdges.Count > 0)
				if (tempHorizontalEdges[tempHorizontalEdges.Count - 1].end == horizontalCollinearEdges[i].start) {
					tempHorizontalEdges[tempHorizontalEdges.Count - 1].end = horizontalCollinearEdges[i].end;
					continue;

				}

			//if not, then add the edge to the list
			tempHorizontalEdges.Add (horizontalCollinearEdges[i]); 

		}
			
		horizontalCollinearEdges = tempHorizontalEdges;

		//Step 18 get rid of collinear lines on the vertical edges
		//
		verticalCollinearEdges = verticalCollinearEdges.OrderBy (e => e.start.y).ToList ();
		verticalCollinearEdges = verticalCollinearEdges.OrderBy (e => e.start.x).ToList ();

		List<Edge> tempVerticalEdges =  new List <Edge> ();

		for (int i = 0; i < verticalCollinearEdges.Count; i++) {

			//if there is anything in the list
			if (tempVerticalEdges.Count > 0)
			if (tempVerticalEdges[tempVerticalEdges.Count - 1].end == verticalCollinearEdges[i].start) {
				tempVerticalEdges[tempVerticalEdges.Count - 1].end = verticalCollinearEdges[i].end;
				continue;

			}

			//if not, then add the edge to the list
			tempVerticalEdges.Add (verticalCollinearEdges[i]); 

		}

		verticalCollinearEdges = tempVerticalEdges;

		drawEdgeCollinearHorizontal = horizontalCollinearEdges;
		drawEdgeCollinearVertical =  verticalCollinearEdges;


		//FINALL STEP:  EXTEND THE EDGES
		tempHorizontalEdges =  new List <Edge> ();

		foreach (Vertex vert in hVertices) {

			if (!vert.isConcave) continue;
		
			bool isCollinear = false;

			foreach (Edge e in horizontalCollinearEdges) {
				if (vert.position == e.start || vert.position == e.end)
					isCollinear = true;
			}

			foreach (Edge e in verticalCollinearEdges) {
				if (vert.position == e.start || vert.position == e.end)
					isCollinear = true;
			}

			if (isCollinear) continue;

			bool isLeft = vert.horizontalNeighbour.position.x > vert.position.x;
			long x = (isLeft) ? long.MinValue : long.MaxValue;


			//max   > left------right < min

			foreach (Edge e in verticalEdges) {

				if (e.start.y < vert.position.y && vert.position.y < e.end.y) {

					if (isLeft && e.start.x > x && e.start.x < vert.position.x)
						x = e.start.x;

					else if (!isLeft && e.start.x < x && e.start.x > vert.position.x)
						x = e.start.x;

				}

			}

			foreach (Edge e in verticalCollinearEdges) {

				if (e.start.y < vert.position.y && vert.position.y < e.end.y) {

					if (isLeft && e.start.x > x && e.start.x < vert.position.x)
						x = e.start.x;

					else if (!isLeft && e.start.x < x && e.start.x > vert.position.x)
						x = e.start.x;

				}

			}

			if (isLeft)
				tempHorizontalEdges.Add (new Edge (vert.position, new FixedVector2 (x, vert.position.y)));
			else
				tempHorizontalEdges.Add (new Edge (new FixedVector2 (x, vert.position.y), vert.position));

		}

		horizontalCollinearEdges.AddRange (tempHorizontalEdges);

		return new BoxCollider[0];

	}

	public enum Direction {

		NORTH,
		WEST,
		SOUTH,
		EAST


	}

	public class Vertex {

		public FixedVector2 position;
		public Vertex horizontalNeighbour;
		public Vertex verticalNeighbour;
		public bool visited = false;
		public bool hasCheckConcavity = false;
		public bool isConcave = false;
		public Direction direction = Direction.NORTH;

		public Vertex (FixedVector2 _position) {

			position = _position;

		}

		public void BecomeHorizontalNeighbours (Vertex other) {

			this.horizontalNeighbour = other;
			other.horizontalNeighbour = this;

		}

		public void BecomeVerticalNeighbours (Vertex other) {

			this.verticalNeighbour = other;
			other.verticalNeighbour = this;

		}

		public bool CheckIfConcave (Direction lastDirection) {

			hasCheckConcavity = true;

			switch (lastDirection) {

			case Direction.NORTH:
				
				//east
				if (horizontalNeighbour.position.x > this.position.x) {

					direction = Direction.EAST;
					return true;

				}
				else 
					direction = Direction.WEST;

				return false;

					

			case Direction.EAST:

				//south
				if (verticalNeighbour.position.y < this.position.y) {

					direction = Direction.SOUTH;
					return true;

				}
				else 
					direction = Direction.NORTH;
				
				return false;

			case Direction.SOUTH:

				//west
				if (horizontalNeighbour.position.x < this.position.x){

					direction = Direction.WEST;
					return true;

				}
				else 
					direction = Direction.EAST;

				return false;

			case Direction.WEST:

				//north
				if (verticalNeighbour.position.y > this.position.y){

					direction = Direction.NORTH;
					return true;

				}
				else 
					direction = Direction.SOUTH;

				return false;
				
			}

			return false;

		}


	}

	public class Edge {

		public FixedVector2 start;
		public FixedVector2 end;

		public Edge (FixedVector2 _p1, FixedVector2 _p2) {


			if (_p1.y == _p2.y) {

				if (_p1.x < _p2.x) {

					start = _p1;
					end = _p2;
					
				} else {

					start = _p2;
					end = _p1;

				}

				
			} else {

				if (_p1.y < _p2.y) {

					start = _p1;
					end = _p2;

				} else {

					start = _p2;
					end = _p1;

				}



			}

		}

		public Edge (Vertex _p1, Vertex _p2) {

			start = _p1.position;
			end = _p2.position;

		}

	}

}
