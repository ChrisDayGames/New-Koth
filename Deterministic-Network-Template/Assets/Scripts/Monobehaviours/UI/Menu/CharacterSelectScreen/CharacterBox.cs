using UnityEngine;
using UnityEngine.UI;

public class CharacterBox : CursorTargetEventPlanner, IGeneratable {

	public Characters character;
    public Image preview;
    public Image border;

	[HideInInspector]
	public Animator anim;

	public void Start () {

		name = Assets.Get (character).name;
		preview.sprite = Assets.Get (character).uiInfo.smallSprite;

	}

    public override void OnEnter(CursorBehaviour cursor) {
        border.color = cursor.GetComponent<Image>().color;
    }

    public override void OnExit(CursorBehaviour cursor) {
        border.color = new Color32(0, 0, 0, 0);
    }

	#if UNITY_EDITOR
    public override void LoadData() {
        base.LoadData();
        anim = GetComponent<Animator>();
    }


	public void OnReset () {
		anim = GetComponent <Animator> ();
	}
	#endif

	//IGeneratable Interface
	public void Generate (int i) {
		
		character = (Characters) i;
		name = Assets.Get (character).name;
		preview.sprite = Assets.Get (character).uiInfo.smallSprite;

	}

	public int GetMaxObjects () {
		return Enum<Characters>.Count;
	}
		
	public MonoBehaviour GetScript () {
		return this;
	}

	public Transform GetTransform () {
		return transform;
	}

}
