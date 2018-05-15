using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXView : View, 
IDangerousListener, 
IDangerousRemovedListener, 
IFreezeListener, 
IFreezeRemovedListener, 
IStunnedListener, 
IStunnedRemovedListener, 
IInvincibleListener, 
IInvincibleRemovedListener, 
IDeadListener, 
IDeadRemovedListener, 
IDirtyListener {

	public PrefabManager prefabManager;

	public override void Link (Entitas.IEntity entity, Entitas.IContext context) {
		base.Link (entity, context);

		var e = (LogicEntity) entity;

		e.AddDangerousListener (this);
		e.AddDangerousRemovedListener (this);
		e.AddFreezeListener(this);
		e.AddFreezeRemovedListener (this);
		e.AddStunnedListener (this);
		e.AddStunnedRemovedListener (this);
		e.AddDangerousRemovedListener (this);
		e.AddInvincibleListener(this);
		e.AddInvincibleRemovedListener(this);
		e.AddDeadListener(this);
		e.AddDeadRemovedListener(this);
		e.AddDirtyListener (this);

	}

	public virtual void OnDirty (LogicEntity entity) {
		
	}

	public virtual void OnDangerous (LogicEntity entity) {
		prefabManager.ManageGameObject ("Fire", true);
        prefabManager.ManageGameObject("Trail", true);
	}

	public virtual void OnDangerousRemoved (LogicEntity entity) {
		prefabManager.ManageGameObject ("Fire", false);
        prefabManager.ManageGameObject("Trail", false);
    }

    public virtual void OnFreeze (LogicEntity entity, int value) {
		sr.material.SetFloat ("_InvertColors", 1);
	}

	public virtual void OnFreezeRemoved (LogicEntity entity) {
		sr.material.SetFloat ("_InvertColors", 0);
	}

	public virtual void OnStunned (LogicEntity entity) {
		prefabManager.ManageGameObject ("Stun_FX", true);
	}

	public virtual void OnStunnedRemoved (LogicEntity entity) {
		prefabManager.ManageGameObject ("Stun_FX", false);
	}

	public virtual void OnInvincible (LogicEntity entity) {
		sr.material.SetColor ("_OverrideColor", new Color (1, 1, 1, 1));
	}

	public virtual void OnInvincibleRemoved (LogicEntity entity) {
		sr.material.SetColor ("_OverrideColor", new Color (1, 1, 1, 0));
	}

	public virtual void OnDead (LogicEntity entity) {
		
		prefabManager.ManageGameObject ("Soul", true);
		prefabManager.InitializeGameObject ("Soul", entity, transform);
	}

	public virtual void OnDeadRemoved (LogicEntity entity) {

	}

	#if UNITY_EDITOR

	public override void GetReferences () {

		base.GetReferences ();

		if (prefabManager == null)
			prefabManager = GetComponent <PrefabManager> ();

	}
	#endif


}
