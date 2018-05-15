using Determinism;
using Entitas;
using UnityEngine;

public class AlphaService {

	public static AlphaService singleton = new AlphaService ();

	Contexts _contexts;

	public void Initialize(Contexts contexts) {
		_contexts = contexts;
	}


	public float GetAlpha () {

		float alpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
		alpha = (alpha > 1) ? 1 : alpha;

		return alpha;

	}

}
