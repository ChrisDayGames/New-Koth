using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager singleton;

	// Use this for initialization
	void Awake () {

        if (singleton == null)
            singleton = this;
        else
            Destroy(this.gameObject);

	}

    public void PostWwiseEvent(string key, GameObject go) {

        //Call the sound
        AkSoundEngine.PostEvent(key, go);

    }

}
