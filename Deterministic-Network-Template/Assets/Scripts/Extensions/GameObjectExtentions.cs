using UnityEngine;

public static class GameObjectExtentions {

	public static T[] GetComponentsInDirectChildren <T> (this GameObject go) {

		int counter = 0;

		foreach (Transform t in go.transform) {

			if (t.GetComponent <T> () != null)
				counter++;

		}

		T[] foundComponents = new T[counter];

		counter = 0;

		foreach (Transform t in go.transform) {

			if (t.GetComponent <T> () != null)
				foundComponents [counter++] = t.GetComponent <T> ();

		}

		return foundComponents;

	}

	public static void ToggleChildren(this Transform transform, bool _enabled) {

		foreach(Transform child in transform) {
			child.gameObject.SetActive(_enabled);
		}

	}

	public static void ToggleChildren(this GameObject go, bool _enabled) {

		ToggleChildren (go.transform, _enabled);

	}

	public static void DestroyChildren(this Transform transform) {

		foreach(Transform child in transform) {
			GameObject.Destroy (child.gameObject);
		}

	}

	public static void DestroyChildren(this GameObject go) {

		DestroyChildren (go.transform);

	}

	public static void DestroyChildrenImmediate(this Transform transform) {

		for (int i = transform.childCount - 1; i >= 0; i--) {
			GameObject.DestroyImmediate (transform.GetChild (i).gameObject);
		}

	}

	public static void DestroyChildrenImmediate(this GameObject go) {

		DestroyChildrenImmediate (go.transform);

	}

}
