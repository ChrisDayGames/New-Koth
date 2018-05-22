using System.Collections.Generic;

[System.Serializable]
public class KothPlayerSelection {

	private List <ISelectionListener> listeners = new List<ISelectionListener> ();

	private Characters character;
	private int skinIndex;
	private int colorIndex;
	private bool isConfirmed;
	private bool isPlaying;

	public Characters Character {

		get {

			return character;

		}

		set {

			if (!isConfirmed && isPlaying) {

				character = value;

				foreach (ISelectionListener listener in listeners)
					listener.OnCharacter (character);

			}

		}

	}

	public int SkinIndex {

		get {

			return skinIndex;

		}

		set {

			skinIndex = value;

			foreach (ISelectionListener listener in listeners)
				listener.OnColor (skinIndex, colorIndex);

		}

	}

	public int ColorIndex {

		get {

			return colorIndex;

		}

		set {

			colorIndex = value;

			foreach (ISelectionListener listener in listeners)
				listener.OnColor (skinIndex, colorIndex);

		}

	}

	public bool IsConfirmed {

		get {

			return isConfirmed;

		}

		set {

			isConfirmed = value;

			foreach (ISelectionListener listener in listeners)
				listener.OnConfirm (isConfirmed);

		}

	}

	public bool IsPlaying {

		get {

			return isPlaying;

		}

		set {

			isPlaying = value;


			foreach (ISelectionListener listener in listeners)
				listener.OnJoin (isPlaying);

		}

	}


	public void AddListener (ISelectionListener listener) {

		listeners.Add (listener);

	}

	public void RemoveListener (ISelectionListener listener) {

		listeners.Remove (listener);

	}

}
