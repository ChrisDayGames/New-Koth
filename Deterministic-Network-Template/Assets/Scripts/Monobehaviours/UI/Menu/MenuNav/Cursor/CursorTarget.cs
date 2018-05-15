using Determinism;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Collections;

public interface ICursorListener {

    void OnCursorAButton(CursorBehaviour cursor);
    void OnCursorBButton(CursorBehaviour cursor);

    void OnCursorEnter(CursorBehaviour cursor);
    void OnCursorOver(CursorBehaviour cursor);
    void OnCursorExit(CursorBehaviour cursor);

}

[System.Serializable]
public class CursorEvent : UnityEvent<CursorBehaviour> { }

[RequireComponent(typeof(RectTransform))]
public abstract class CursorTarget : UICollider, ICursorListener {

    public CursorEvent CursorAButtonEvent;
    public CursorEvent CursorBButtonEvent;
    public CursorEvent CursorEnterEvent;
    public CursorEvent CursorOverEvent;
    public CursorEvent CursorExitEvent;

    [HideInInspector]
    public bool[] collidingCursors;

    void Start() {
		collidingCursors = new bool[GameConstants.MAX_PLAYERS];
    }

    public override void Init() {
        base.Init();
        CursorBehaviour.cursorTargets.Add(this);
    }

    public virtual void OnCursorAButton(CursorBehaviour cursor) { 
        //Debug.Log("A");
        CursorAButtonEvent.Invoke(cursor);
    }

    public virtual void OnCursorBButton(CursorBehaviour cursor) { 
        //Debug.Log("B");
        CursorBButtonEvent.Invoke(cursor);
    }

    public virtual void OnCursorEnter(CursorBehaviour cursor) { 
        //Debug.Log("Enter");
        CursorEnterEvent.Invoke(cursor);
    }

    public virtual void OnCursorOver(CursorBehaviour cursor) { 
        //Debug.Log("Over");
        CursorOverEvent.Invoke(cursor);
    }

    public virtual void OnCursorExit(CursorBehaviour cursor) {
        //Debug.Log("Exit");
        CursorExitEvent.Invoke(cursor);
    }

}