using UnityEngine;
using Entitas;
using Determinism;

public class LevelView : MonoBehaviour, ILevelListener, ICollisionInfoListener {

	private MeshJigglifier jiggler;
	private GameObject levelMesh;
	private GameObject skybox;

	public void Start () {
		Contexts.sharedInstance.logic.CreateEntity ().AddLevelListener (this);
		Contexts.sharedInstance.logic.CreateEntity ().AddCollisionInfoListener (this);
	}

	public void OnLevel (LogicEntity e, LevelData data) {

		Destroy (levelMesh);
		Destroy (skybox);

		levelMesh = LevelBuilder.GenerateGameObject (e.level.data);
		levelMesh.transform.parent = transform;
		jiggler = levelMesh.AddComponent <MeshJigglifier> ();

		skybox = GameObject.Instantiate (Assets.Get (e.level.data.environment).skybox);
		skybox.transform.parent = transform;

	}

	public void OnCollisionInfo (LogicEntity e, RaycastCollisionInfo info) {


		if (info.horizontalEntered) {

			jiggler.Splash (
				e.position.value.ToVector3 (), 
				e.lastVelocity.value.ToVector3 () / 200,
				10
			);

		}

		if (info.verticalEntered) {

			jiggler.Splash (
				e.position.value.ToVector3 (), 
				e.lastVelocity.value.ToVector3 () / 200,
				10
			);
			
		}

		if (info.horizontalExited) {

			jiggler.Splash (
				e.position.value.ToVector3 (), 
				e.lastVelocity.value.ToVector3 () / 200,
				10
			);

		} 

		if (info.VerticalExited) {

			jiggler.Splash (
				e.position.value.ToVector3 (), 
				e.lastVelocity.value.ToVector3 () / 200,
				10
			);

		}

	}

}
