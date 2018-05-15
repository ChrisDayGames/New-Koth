using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

[RequireComponent(typeof(CursorTarget))]
public abstract class CursorTargetEventPlanner : MonoBehaviour {


    public virtual void OnEnter(CursorBehaviour cursor) {
        
    }

    public virtual void OnOver(CursorBehaviour cursor) {

    }

    public virtual void OnExit(CursorBehaviour cursor) {

    }

    public virtual void OnAButton(CursorBehaviour cursor) {
        
    }

    public virtual void OnBButton(CursorBehaviour cursor) {
        
    }

#if UNITY_EDITOR
    public virtual void LoadData() {

        CursorTarget target = GetComponent<CursorTarget>();

        UnityEventTools.RemovePersistentListener<CursorBehaviour>(target.CursorEnterEvent, OnEnter);
        UnityEventTools.RemovePersistentListener<CursorBehaviour>(target.CursorOverEvent, OnOver);
        UnityEventTools.RemovePersistentListener<CursorBehaviour>(target.CursorExitEvent, OnExit);
        UnityEventTools.RemovePersistentListener<CursorBehaviour>(target.CursorAButtonEvent, OnAButton);
        UnityEventTools.RemovePersistentListener<CursorBehaviour>(target.CursorBButtonEvent, OnBButton);

        UnityEventTools.AddPersistentListener(target.CursorEnterEvent, OnEnter);
        UnityEventTools.AddPersistentListener(target.CursorOverEvent, OnOver);
        UnityEventTools.AddPersistentListener(target.CursorExitEvent, OnExit);
        UnityEventTools.AddPersistentListener(target.CursorAButtonEvent, OnAButton);
        UnityEventTools.AddPersistentListener(target.CursorBButtonEvent, OnBButton);

    }

    void OnValidate() {
		if (Application.isPlaying) return;
        LoadData();
    }
#endif

}
