using Determinism;
using CommandInput;
using UnityEngine;
using UnityEngine.UI;


public class GeneralNavigationBehaviour : MenuInputBehaviour {

    [Header("Defaults")]
    public int defaultSectionIndex;
    public int defaultIndex;

    [Header("Selector")]
    public RectTransform selector;
    public int selectorPadding;

    //index used for scrolling options
    protected int index;
    protected bool canMoveX;
    protected bool canMoveY;

    protected override void Init() {
        
    }

    protected override void Move (float x, float y) {

		if (Mathf.Abs (x) < 0.3f) {

			canMoveX = true;
			return;

		} else if  (!canMoveX) return;

		index += (int) Mathf.Sign (x);
		//index = Mathf.Clamp (index, 0, sections[sectionIndex].sectionOptions.Length - 1);
		canMoveX = false;

		base.Move (x, y);

	}

    protected override void AButton (CommandInput.ButtonSnapshot aButton) {
		base.AButton (aButton);

		if (aButton.down) {
            //sectionOptions[index].GetComponent<Button>().onClick.Invoke();
            //menuOptions[sectionIndex][index].GetComponent<Button>().onClick.Invoke();
        }

    }

	protected override void BButton (ButtonSnapshot bButton) {
		base.BButton (bButton);

	}

}