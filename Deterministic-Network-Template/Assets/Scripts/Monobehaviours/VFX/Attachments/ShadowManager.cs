using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour {
    
	public const float MAX_SIZE_DISTANCE = 10f;
	public const float MAX_SIZE_FACTOR = 2f;

	public LayerMask solidMask;

	public Vector2 positionOffset = Vector2.zero;
	public Vector2 scale = Vector2.right;
	public Vector2 checkOffset = Vector2.right;

	public Sprite shadow;
	public Color32 shadowColor = new Color32(0x0F, 0x32, 0x5A, 0x66);

	private Vector3 intialPosition;
	private Vector3 initialScale;

	private GameObject leftShadow;
	private GameObject rightShadow;

	private Material leftMat;
	private Material rightMat;

	// Use this for initialization
	void Start () {

		initialScale = scale;

		leftShadow = new GameObject ("Left Shadow");
		leftShadow.transform.parent = this.transform;
		SpriteRenderer left = leftShadow.AddComponent<SpriteRenderer> ();
		left.sprite = shadow;
		left.sortingLayerName = "Game";
		left.sortingOrder = -1;
		left.material = new Material (Shader.Find("Sprites/Shadow"));
		leftMat = left.material;
		leftMat.color = shadowColor;


        rightShadow = new GameObject ("Right Shadow");
		rightShadow.transform.parent = this.transform;
		SpriteRenderer right = rightShadow.AddComponent<SpriteRenderer> ();
		right.sprite = shadow;
		right.sortingLayerName = "Game";
		right.sortingOrder = -1;
		right.material = new Material (Shader.Find("Sprites/Shadow"));
		rightMat = right.material;
		rightMat.color = shadowColor;
		
	}
	
	// Update is called once per frame
	void Update () {
		CastShadow ();
	}

	void CastShadow () {

		//checking the left and right sides of the object
		RaycastHit2D hitLeft = Physics2D.Raycast (transform.position - new Vector3 (checkOffset.x, checkOffset.y, 0f), Vector3.down, 10000, solidMask);
		RaycastHit2D hitRight = Physics2D.Raycast (transform.position + new Vector3 (checkOffset.x, checkOffset.y, 0f), Vector3.down, 10000, solidMask);

		Debug.DrawRay (transform.position - new Vector3 (checkOffset.x, checkOffset.y, 0f), Vector3.down * 100);
		Debug.DrawRay (transform.position + new Vector3 (checkOffset.x, checkOffset.y, 0f), Vector3.down * 100);

		leftShadow.transform.localScale = initialScale + Mathf.Clamp01 (hitLeft.distance / MAX_SIZE_DISTANCE) * initialScale * (MAX_SIZE_FACTOR - 1);
		leftShadow.transform.position = new Vector2 (transform.position.x + positionOffset.x, hitLeft.point.y + positionOffset.y);

		rightShadow.transform.localScale = initialScale + Mathf.Clamp01 (hitRight.distance / MAX_SIZE_DISTANCE) * initialScale * (MAX_SIZE_FACTOR - 1);
		rightShadow.transform.position = new Vector2 (transform.position.x + positionOffset.x, hitRight.point.y + positionOffset.y);

		if (!Mathf.Approximately(hitLeft.distance, hitRight.distance)) {
			
			RaycastHit2D higherHit = (hitLeft.distance < hitRight.distance) ? hitLeft : hitRight;
			RaycastHit2D lowerHit = (hitLeft.distance > hitRight.distance) ? hitLeft : hitRight;
			Vector2 checkPoint = new Vector2 (lowerHit.point.x, higherHit.point.y - 0.5f);

			RaycastHit2D splitLength = Physics2D.Raycast (checkPoint,  (hitLeft.distance > hitRight.distance) ? Vector3.right : Vector3.left, 10000, solidMask);
			Debug.DrawRay (checkPoint, ((hitLeft.distance > hitRight.distance) ? Vector3.right : Vector3.left) * 100, Color.black);

			float totalDistance = checkOffset.x * 2;
			float splitDistance = splitLength.distance;
			float splitValue = (splitDistance /totalDistance);

			if (hitLeft.distance < hitRight.distance)
				splitValue = 1 - splitValue;
				

			leftMat.SetFloat ("_SliceAmountLeft", 0);
			leftMat.SetFloat ("_SliceAmountRight", splitValue);

			rightMat.SetFloat ("_SliceAmountLeft", splitValue);
			rightMat.SetFloat ("_SliceAmountRight", 1);

			rightShadow.SetActive(true);

			//Debug.DrawLine (checkPoint, splitLength.point, Color.blue);

		} else {

			leftMat.SetFloat ("_SliceAmountLeft", 0);
			leftMat.SetFloat ("_SliceAmountRight", 1);

			rightMat.SetFloat ("_SliceAmountLeft", 0);
			rightMat.SetFloat ("_SliceAmountRight", 1);

			rightShadow.SetActive(false);
			
		}

		leftShadow.transform.rotation = Quaternion.identity;
		rightShadow.transform.rotation = Quaternion.identity;

	}

}
