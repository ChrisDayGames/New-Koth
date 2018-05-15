//source = https://forum.unity.com/threads/pixel-perfect-plus-perspective-camera.213921/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelCam : MonoBehaviour {

	public Camera cam;
	[Range (1, 16)]
	public float zoom;

	[Range (1, 128)]
	public int ppu = Pixels.PIXELS_PER_UNIT;

	public Transform parent;

	private Vector3 bottomLeft;
	private Vector3 topRight;

	private float camWorldWidth;
	private float camWorldHeight;


	private float initialZoom;
	private float newScale;

	public void Start () {
		cam = Camera.main;

		initialZoom = zoom;
	}

	public void LateUpdate () {

		if (cam != null)
			PixelSnap (); 


		if (Input.GetKeyDown (KeyCode.Equals))
			ppu++;

		if (Input.GetKeyDown (KeyCode.Minus))
			ppu--;

	}

	public void PixelSnap () {

		Pixels.SCALED_PPU = ppu;
		Debug.Log (1f / Pixels.SCALED_PPU);

		var frustrumInnerAngle = (180f - cam.fieldOfView) / 2f * Mathf.PI / 180;

		var targetFrustrumSize = (float) Screen.height / (float)ppu;
		var newCamDist = (Mathf.Tan (frustrumInnerAngle) * (targetFrustrumSize / 2f));

		transform.position = new Vector3 (
			transform.position.x.PixelRound (),
			transform.position.y.PixelRound (),
			-newCamDist);

		if (parent == null) return;

		Vector3 bottomLeft = cam.ViewportToWorldPoint (new Vector3 (0, 0, -cam.transform.position.z));

//		parent.localScale = new Vector3 (
//
//			(float) ppu/Pixels.PIXELS_PER_UNIT,
//			(float) ppu/Pixels.PIXELS_PER_UNIT,
//			//Pixels.CalculateScaleFromPPU (ppu).ContrainToPixelScale (),
//			//Pixels.CalculateScaleFromPPU (ppu).ContrainToPixelScale (),
//			1
//		
//		);

		parent.position = bottomLeft;
		parent.position = new Vector3 (

			parent.transform.position.x.PixelRound (),
			parent.transform.position.y.PixelRound (),
			0

		);

	}

	public void OnDrawGizmos () {

		Vector3 bottomLeft = cam.ViewportToWorldPoint (new Vector3 (0, 0, -cam.transform.position.z));
		Vector3 topRight = cam.ViewportToWorldPoint (new Vector3 (1, 1, -cam.transform.position.z));

		float worldWidth = topRight.x - bottomLeft.x;
		float worldHeight = topRight.y - bottomLeft.y;

		Gizmos.color = Color.white;
		Gizmos.DrawWireCube (
			new Vector3 (cam.transform.position.x, cam.transform.position.y, 0),
			new Vector3 (worldWidth, worldHeight, 0)
		);

		for (int i = 0; i < worldHeight; i++) {

			Gizmos.DrawLine (bottomLeft + Vector3.up * i, bottomLeft + Vector3.up * i + Vector3.right * worldWidth);

		}

		for (int i = 0; i < worldWidth; i++) {

			Gizmos.DrawLine (bottomLeft + Vector3.right * i, bottomLeft + Vector3.right * i + Vector3.up * worldWidth);

		}

	}

}
