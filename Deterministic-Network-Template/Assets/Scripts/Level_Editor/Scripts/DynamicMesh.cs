using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Determinism;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class DynamicMesh : MonoBehaviour {
	
	MeshFilter filter;
	MeshRenderer rend;
	PolygonCollider2D col;

	public void Awake () {

		filter = GetComponent <MeshFilter> ();
		rend = GetComponent <MeshRenderer> ();
		//col = go.AddComponent <PolygonCollider2D> ();
		//meshToCol = go.AddComponent <Mesh2DColliderMaker> ();

	}

	public void CreateMesh (Mesh mesh, Texture2D texture, Material mat) {

		if (filter == null) filter = gameObject.AddComponent <MeshFilter> ();
		if (rend == null) rend = gameObject.AddComponent <MeshRenderer> ();
		//if (col == null) col = gameObject.AddComponent <PolygonCollider2D> ();
		//if (meshToCol == null) meshToCol = gameObject.AddComponent <Mesh2DColliderMaker> ();

		filter.mesh = mesh;
		rend.material = mat;
		rend.material.mainTexture = texture;

	}

	public void UpdateMesh (Mesh mesh) {

		if (filter == null) filter = gameObject.AddComponent <MeshFilter> ();
		if (rend == null) rend = gameObject.AddComponent <MeshRenderer> ();


		filter.mesh = mesh;

	}

	public void CreateCollider () {

//		if (col == null) col = gameObject.AddComponent <PolygonCollider2D> ();
//		if (meshToCol == null) meshToCol = gameObject.AddComponent <Mesh2DColliderMaker> ();
//		
//		col.isTrigger = false;
//		meshToCol.CreatePolygon2DColliderPoints(filter.mesh);

	}
		
}
