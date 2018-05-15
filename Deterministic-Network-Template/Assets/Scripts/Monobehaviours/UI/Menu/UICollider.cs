using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Determinism;

[RequireComponent(typeof(RectTransform))]
public class UICollider : MonoBehaviour {

	public RectTransform rect;
    public BoundingBox box;

    void Awake() {
        Init();
    }

    public virtual void Init() {
        box = new BoundingBox(new FixedVector2(rect.position), new FixedVector2(rect.sizeDelta));
    }

    void Update() {
        Resize();
        Move();
    }

    public virtual void Move() {
        box.position = new FixedVector2(rect.position);
    }

    public virtual void Resize() {
        box.size = new FixedVector2(rect.sizeDelta);
    }

    public virtual bool IsCollidingWith(CursorBehaviour cursor) { return false; }

    void OnValidate() {
		if (Application.isPlaying) return;
        rect = GetComponent<RectTransform>();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(box.position.ToVector3(), box.size.ToVector3());
    }

}
