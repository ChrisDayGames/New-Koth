using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIConstrainToScreen : MonoBehaviour {

    [HideInInspector]
    public RectTransform rect;

    void LateUpdate() {

        float x = rect.anchoredPosition.x;
        float y = rect.anchoredPosition.y;

        if (Mathf.Abs(x) > UIScale.CANVAS_SIZE.x / 2) {
            x = UIScale.CANVAS_SIZE.x / 2 * Mathf.Sign(x);
        }

        if (Mathf.Abs(y) > UIScale.CANVAS_SIZE.y / 2) {
            y = UIScale.CANVAS_SIZE.y / 2 * Mathf.Sign(y);
        }

        rect.anchoredPosition = new Vector2(x, y);

    }

    void OnValidate() {
		if (Application.isPlaying) return;
        rect = GetComponent<RectTransform>();
    }

}