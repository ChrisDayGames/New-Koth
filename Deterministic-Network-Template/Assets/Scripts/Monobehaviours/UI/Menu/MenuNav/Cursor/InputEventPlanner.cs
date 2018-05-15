using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

[RequireComponent(typeof(PlayerInputListener))]
public abstract class InputEventPlanner : MonoBehaviour {

	public PlayerInputListener listener;

    public virtual void OnMove(Vector2 input) { }

    public virtual void OnAButton() {}

	public virtual void OnBButton() {}

	public virtual void OnLButton() {}

	public virtual void OnRButton() {}

	public virtual void OnXButton() {}

	public virtual void OnYButton() {}

	public virtual void OnStartButton() {}

#if UNITY_EDITOR

    public virtual void LoadData() {
        listener = GetComponent<PlayerInputListener>();

		UnityEventTools.RemovePersistentListener(listener.CursorAButtonEvent, OnAButton);
		UnityEventTools.RemovePersistentListener(listener.CursorBButtonEvent, OnBButton);
		UnityEventTools.RemovePersistentListener(listener.CursorRButtonEvent, OnRButton);
		UnityEventTools.RemovePersistentListener(listener.CursorLButtonEvent, OnLButton);
        UnityEventTools.RemovePersistentListener(listener.CursorXButtonEvent, OnXButton);
        UnityEventTools.RemovePersistentListener(listener.CursorYButtonEvent, OnYButton);
		UnityEventTools.RemovePersistentListener(listener.CursorStartButtonEvent, OnStartButton);

        UnityEventTools.RemovePersistentListener<Vector2>(listener.CursorMoveEvent, OnMove);

        UnityEventTools.AddPersistentListener(listener.CursorAButtonEvent, OnAButton);
        UnityEventTools.AddPersistentListener(listener.CursorBButtonEvent, OnBButton);
		UnityEventTools.AddPersistentListener(listener.CursorRButtonEvent, OnRButton);
		UnityEventTools.AddPersistentListener(listener.CursorLButtonEvent, OnLButton);
		UnityEventTools.AddPersistentListener(listener.CursorXButtonEvent, OnXButton);
		UnityEventTools.AddPersistentListener(listener.CursorYButtonEvent, OnYButton);
		UnityEventTools.AddPersistentListener(listener.CursorStartButtonEvent, OnStartButton);

        UnityEventTools.AddPersistentListener(listener.CursorMoveEvent, OnMove);

    }

    void OnValidate() {
		if (Application.isPlaying) return;
        LoadData();
    }

#endif

}
