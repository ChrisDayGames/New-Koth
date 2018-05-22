using UnityEngine;
using UnityEngine.UI;
using ModularUI;

public class ChangeColorOnHover : MonoBehaviour, IHoverable {

	[HideInInspector]
	public Graphic graphic;
	public Color hoverColor;

	private Color initialColor;

	void Awake () {

		initialColor = graphic.color;

	}


	#region IHoverable implementation

	public void OnHoverBegin () {
	
		graphic.color = hoverColor;

	}

	public void OnHoverOver () {
		


	}
	public void OnHoverEnd () {
		
		graphic.color = initialColor;

	}

	#endregion

	void Reset () {

		graphic = GetComponent <Graphic> ();

	}

}
