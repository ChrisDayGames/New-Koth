using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Determinism;
using EZCameraShake;

public class CameraView : ManagedBehaviour, ILevelListener, ICollisionInfoListener {

	public static List<Transform> followTargets = new List<Transform> ();
	public static float minZ = -20;
	public static float maxZ = -35;

	public float borderWidth = 13f;
	public float minSize = 7;
	public float maxSize = 21;
	public float moveTime = 0.3f;

	private Camera cam;
	private Rect cameraBounds;
	private Vector3 desiredPositionSmooth; 

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		cameraBounds = new Rect ();
		Contexts.sharedInstance.logic.CreateEntity ().AddLevelListener (this);
		Contexts.sharedInstance.logic.CreateEntity ().AddCollisionInfoListener (this);
	}
	
	// Update is called once per frame
	public override void Execute (float alpha) {

		if (followTargets.Count == 0) return;
		if (cam == null) return;
		if (!cam.gameObject.activeInHierarchy) return;

		Vector3 centroid = Vector3.zero;
		Vector3 lastPos = cam.transform.root.position;

		foreach (Transform target in followTargets) {

			if (target.gameObject.activeInHierarchy)
				centroid += target.position;
			
		}

		centroid /= followTargets.Count;
		centroid.z = CalculateZoom (centroid);
		centroid = ClampCentroid (centroid);


		transform.position = Vector3.SmoothDamp (
			transform.position, 
			centroid, 
			ref desiredPositionSmooth, 
			moveTime);

	}

	public void OnLevel (LogicEntity e, LevelData data) {

		cameraBounds.min = new Vector2 (data.minX, data.minY);
		cameraBounds.max = new Vector2 (data.maxX, data.maxY);
			
	}

	public void OnCollisionInfo (LogicEntity e, RaycastCollisionInfo info) {

		if (e.isDangerous) {

			//shake if other is player
			//or other is hat and is attached

//			float distanceModifier = 1 - Mathf.Clamp01 ((transform.position.z - minZ) / (maxZ - minZ));
//			CameraShaker.Instance.ShakeOnce (
//				6f,
//				1f, 
//				0.1f, 
//				0.2f
//			);

		}

	}

	private float CalculateZoom (Vector3 centroid) {

		centroid.z = 0;

		Vector3 desiredLocalPosition = cam.transform.InverseTransformPoint(centroid);
		float distance = 0;

		foreach (Transform target in followTargets) {
			
			if (target.gameObject.activeInHierarchy) {

				Vector3 localTargetPosition = cam.transform.InverseTransformPoint (target.position);
				Vector3 distanceToTarget = (desiredLocalPosition - localTargetPosition);

				distance = Mathf.Max (distance, Mathf.Abs (distanceToTarget.y) * cam.aspect);
				distance = Mathf.Max (distance, Mathf.Abs (distanceToTarget.x));

			}
				centroid += target.position;
		}
			
		distance = Mathf.Max (distance, minSize);
		distance = Mathf.Min (distance, maxSize);

		return -distance - borderWidth;

	}

	private Vector3 ClampCentroid (Vector3 centroid) {

		float minX = cameraBounds.xMin;
		float maxX = cameraBounds.xMax;
		float minY = cameraBounds.yMin;
		float maxY = cameraBounds.yMax;

		Vector3 topLeftCorner = cam.ViewportToWorldPoint (new Vector3 (0,0, centroid.z));
		Vector3 bottomRightCorner = cam.ViewportToWorldPoint (new Vector3 (1,1, centroid.z));

		float camWidth = Mathf.Abs(topLeftCorner.x -bottomRightCorner.x);
		float camHeight = Mathf.Abs(topLeftCorner.y -bottomRightCorner.y);

		float topBound = centroid.y + camHeight/2;
		float bottomBound = centroid.y - camHeight/2;

		float leftBound = centroid.x - camWidth/2;
		float rightBound = centroid.x + camWidth/2;

		if (bottomBound <= minY) {
			centroid.y = minY + camHeight/2;
		} else if (topBound >= maxY) {
			centroid.y = maxY - camHeight/2;
		}

		if (leftBound <= minX) {
			centroid.x = minX + camWidth/2;
		} else if (rightBound >= maxX) {
			centroid.x = maxX - camWidth/2;
		}

		topBound = centroid.y + camHeight/2;
		bottomBound = centroid.y - camHeight/2;

		leftBound = centroid.x - camWidth/2;
		rightBound = centroid.x + camWidth/2;

		if (bottomBound <= minY && topBound >= maxY) {
			centroid.y = 0;
		}

		if (leftBound <= minX && rightBound >= maxX) {
			centroid.x = 0;
		}

		return centroid;

	}

}
