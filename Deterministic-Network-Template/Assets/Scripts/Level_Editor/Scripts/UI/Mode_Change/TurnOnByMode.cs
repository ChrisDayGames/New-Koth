using Determinism;
using Entitas;

namespace LevelEditor {

	public class TurnOnByMode : ModeListenerBehaviour {

		public bool isEditorObj;

		public override void OnModeChanged (bool isEditMode) {

			gameObject.SetActive (isEditorObj == isEditMode);

		}

	}
		
}