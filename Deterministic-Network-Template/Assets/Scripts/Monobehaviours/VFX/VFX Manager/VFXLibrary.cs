using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXLibrary : MonoBehaviour {

	//public ParticleGroup[] particleGroups;
	public List <ParticleGroup> particleGroups;

	Dictionary<string, ParticleSystem> groupDictionary = new Dictionary<string, ParticleSystem> ();

	void Awake () {
	
		foreach (ParticleGroup particleGroup in particleGroups) {
			groupDictionary.Add (particleGroup.groupID, particleGroup.system);
		}

	}

	void OnValidate () {

		if (Application.isPlaying) return;

		particleGroups.Clear ();
		LoadParticleGroupsFromChildren (this.transform);

	}

	public void LoadParticleGroupsFromChildren (Transform t) {

		for (int i = 0; i < t.childCount; i++) {

			Transform child = t.GetChild(i);
			ParticleSystem ps = child.GetComponent<ParticleSystem> ();

			if (ps != null)
				particleGroups.Add (new ParticleGroup (child.name, ps));

			if (child.childCount > 0)
				LoadParticleGroupsFromChildren (child);
			

		}

	}

	public ParticleSystem GetParticleSystemFromName (string name) {
		if (groupDictionary.ContainsKey (name)) {
			ParticleSystem system = groupDictionary [name];
			system.gameObject.SetActive (true);
			return system;
		}
		return null;
	}

	[System.Serializable]
	public class ParticleGroup {
		public string groupID;
		public ParticleSystem system;

		public ParticleGroup (string _groupID, ParticleSystem _system) {

			this.groupID = _groupID;
			this.system = _system;

		}

	}

}
