using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenBehaviour : MonoBehaviour, IMenuStateListener {

	public MenuState screenName;

	//private reference to the time context
	protected MetaContext _context;

	// Use this for initialization
	void Start () {

		//get the time context
		_context = Contexts.sharedInstance.meta;

		//creating an entity to represent this
		MetaEntity e = _context.CreateEntity();

		//add the appropriate listeners
		e.AddMenuStateListener (this);

		OnMenuState (e, MenuState.INTRO);
	}

	public void OnMenuState (MetaEntity e, MenuState state) {

		this.gameObject.SetActive (screenName == state);

	}

}
