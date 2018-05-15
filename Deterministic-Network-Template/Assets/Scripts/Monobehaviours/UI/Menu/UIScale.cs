using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasScaler))]
public class UIScale : MonoBehaviour {

    public static float SIZE_FACTOR {
        get { return sizeFactor; }
    }

    public static Vector2 REFERENCE_RESOLUTION {
        get { return referenceResolution; }
    }

    public static Vector2 CANVAS_SIZE {
        get { return canvasScale; }
    }

    private static float sizeFactor = 1f;
    private static Vector2 referenceResolution;
    private static Vector2 canvasScale;

	[SerializeField]
    private CanvasScaler canvasScaler;
    private Vector2 lastResolution;

	[SerializeField]
    private RectTransform canvasRect;

    void Awake() {
        Resize();
    }

    void Update() {

        canvasScale = canvasRect.sizeDelta;

        if (lastResolution != new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight))
            Resize();
    }

    void Resize() {
        referenceResolution = canvasScaler.referenceResolution;
        lastResolution = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        sizeFactor = lastResolution.y / referenceResolution.y;
    }

    void OnValidate() {
		if (Application.isPlaying) return;
        canvasScaler = GetComponent<CanvasScaler>();
        canvasRect = GetComponent<RectTransform>();
    }

}
