using UnityEngine;
using ModularUI;
using UnityEngine.UI;

public class UIConstrainToScreen : UIBehaviour {

	[SerializeField]
	private RectTransform canvasRect;

	[SerializeField]
	private CanvasScaler canvasScaler;

	void Start () {

		canvasScaler = GetComponentInParent <CanvasScaler> ();
		canvasRect = canvasScaler.GetComponentInParent <RectTransform> ();

	}

    void LateUpdate() {

		float x = rectTransform.anchoredPosition.x;
		float y = rectTransform.anchoredPosition.y;
		float width = (canvasRect.sizeDelta.x * canvasScaler.referenceResolution.x) / 2;
		float height = (canvasRect.sizeDelta.y * canvasScaler.referenceResolution.y) / 2;

		if (Mathf.Abs(x) > width) {
			x = width / 2 * Mathf.Sign(x);
        }

		if (Mathf.Abs(y) > height) {
			y = height * Mathf.Sign(y);
        }

		rectTransform.anchoredPosition = new Vector2(x, y);

    }

	public override void GetReferences () {
		base.GetReferences ();

		canvasScaler = GetComponentInParent <CanvasScaler> ();
		canvasRect = canvasScaler.GetComponentInParent <RectTransform> ();


	}

}