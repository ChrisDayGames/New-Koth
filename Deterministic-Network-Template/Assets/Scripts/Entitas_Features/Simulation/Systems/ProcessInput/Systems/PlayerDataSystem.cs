using System;
using Entitas;

[Serializable]
public class PlayerData {

    public Characters character;
    public Teams team;
    public int skinIndex;
    public int colorIndex;

    public PlayerData() {
        this.character = Characters.BIRTHDAY;
        this.team = Teams.NONE;
        this.skinIndex = 0;
        this.colorIndex = 0;
    }

    public PlayerData(Characters _character, Teams _team, int _skinIndex, int _colorIndex) {
        this.character = _character;
        this.team = _team;
        this.skinIndex = _skinIndex;
        this.colorIndex = _colorIndex;
    }
    
}

[Serializable]
public class InGameData {

    public int score;
    public int lives;
    public int rank;

}

public class PlayerDataSystem : IInitializeSystem {

	readonly InputContext _inputContext;

	public PlayerDataSystem (Contexts contexts) {

		_inputContext = contexts.input;

    }

    public void Initialize() {

		//_inputContext.ReplacePlayerData(new PlayerData[GameConstants.MAX_PLAYERS]);

    }

}
