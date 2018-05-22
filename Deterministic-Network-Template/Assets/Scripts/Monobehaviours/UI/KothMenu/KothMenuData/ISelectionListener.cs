public interface ISelectionListener {

	void OnCharacter (Characters character);
	void OnColor (int skinIndex, int colorIndex);
	void OnConfirm (bool isConfirmed);
	void OnJoin (bool isJoined);

}
