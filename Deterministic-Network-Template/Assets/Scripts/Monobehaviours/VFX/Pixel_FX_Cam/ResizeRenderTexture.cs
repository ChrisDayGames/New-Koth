using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Camera))]
public class ResizeRenderTexture : MonoBehaviour {

	private Camera cam;
	private Material mat;
	private RenderTextureDescriptor rtInstructions;

	public void Awake () {

		cam = GetComponent <Camera> ();
		mat = transform.GetChild (0).GetComponent <Renderer> ().sharedMaterial;
		mat.mainTexture = cam.targetTexture;

		rtInstructions = new RenderTextureDescriptor ();
		rtInstructions.dimension = UnityEngine.Rendering.TextureDimension.Tex2D;
		rtInstructions.colorFormat = RenderTextureFormat.ARGB32;


		rtInstructions.sRGB = false;
		rtInstructions.useMipMap = false;
		rtInstructions.autoGenerateMips = false;

		rtInstructions.depthBufferBits = 0;
		rtInstructions.memoryless = RenderTextureMemoryless.None;
		rtInstructions.msaaSamples = 1;
		rtInstructions.volumeDepth = 1;
		
	}

	public void Update () {

		Destroy (cam.targetTexture);

		Vector3 bottomLeft = cam.ViewportToWorldPoint (new Vector3 (0, 0, cam.nearClipPlane - cam.transform.position.z));
		Vector3 topRight = cam.ViewportToWorldPoint (new Vector3 (1, 1, cam.nearClipPlane - cam.transform.position.z));

		//rtInstructions.width = 1 + (int) Mathf.Abs (bottomLeft.x - topRight.x) * 15;
		rtInstructions.height = 1 + 3 * ((int) Mathf.Abs (bottomLeft.y - topRight.y) * 8) / 2;
		rtInstructions.width = 1 + rtInstructions.height * 2;

		cam.targetTexture = new RenderTexture (rtInstructions);
		cam.targetTexture.filterMode = FilterMode.Point;
		mat.mainTexture = cam.targetTexture;
		
	}


	public void OnValidate () {

		if (Application.isPlaying) return;

		cam = GetComponent <Camera> ();
		mat = transform.GetChild (0).GetComponent <Renderer> ().sharedMaterial;
		mat.mainTexture = cam.targetTexture;
		
	}

}
