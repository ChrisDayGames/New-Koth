using System.Collections.Generic;
using UnityEngine;
using CommandInput;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(UICollider))]
[RequireComponent(typeof(RectTransform))]
public class CursorBehaviour : MenuInputBehaviour, IGeneratable {

    public static List<CursorTarget> cursorTargets = new List<CursorTarget>();
	public static CursorBehaviour[] allCursors = new CursorBehaviour[GameConstants.MAX_PLAYERS];

    [HideInInspector]
    public UICollider col;
    public float speed = 800f;

    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public CursorTarget currentTarget;

    protected override void Init() {
        base.Init();
        allCursors[playerId] = this;
    }

	public override void Execute (float alpha) {

        foreach(CursorTarget target in cursorTargets) {
            if (target.IsCollidingWith(this)) {
                
                if(!target.collidingCursors[playerId]) {
                    target.collidingCursors[playerId] = true;
                    //collision enter
                    target.OnCursorEnter(this);

                } else {
                    //colliding
                    target.OnCursorOver(this);

                }

                currentTarget = target;

            } else {

                if (target.collidingCursors[playerId]) {
                    target.collidingCursors[playerId] = false;
                    //collision exit
                    target.OnCursorExit(this);

                } else {
                    //not colliding

                }
            }
        }
    }

    protected override void Move(float x, float y) {
        float factor = speed * Time.unscaledDeltaTime;
        rect.Translate(new Vector3(x, y, 0).normalized * factor);
    }

    protected override void AButton(ButtonSnapshot aButton) { 
        if(aButton.down) {
            if(currentTarget != null) {
                currentTarget.OnCursorAButton(this);
            }
        }
        
        if(aButton.pressed) {

        } 
        
        if(aButton.up) {

        }
    }

    protected override void BButton(ButtonSnapshot bButton) {
        if (bButton.down) {
            if (currentTarget != null) {
                currentTarget.OnCursorBButton(this);
            }
        }

        if (bButton.pressed) {

        }

        if (bButton.up) {

        }
    }

    void OnValidate() {
		if (Application.isPlaying) return;
        rect = GetComponent<RectTransform>();
        col = GetComponent<UICollider>();
    }

    public void Generate(int i) {
        this.playerId = i;

        GetComponentInChildren<Text>().text = "P" + (i + 1);
    }

    public int GetMaxObjects() {
        return GameConstants.MAX_PLAYERS;
    }

    public MonoBehaviour GetScript() {
        return this;
    }

    public Transform GetTransform() {
        return transform;
    }
}