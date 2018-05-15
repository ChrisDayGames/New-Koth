using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBox : InputEventPlanner, IGeneratable {

    public static PlayerBox[] playerBoxes = new PlayerBox[GameConstants.MAX_PLAYERS];

	public int playerId {

		get {return listener.playerId;}
		set {if (value >= 0 && value < GameConstants.MAX_PLAYERS) listener.playerId = value;}

	}

    public bool isReady { get { return hasMadeSelection || !hasJoinedTheGame; } }
    
    public static bool allPlayersReady { 
    
        get {

            for (int i = 0; i < GameConstants.MAX_PLAYERS; i++)
                if (playerBoxes[i] != null && !playerBoxes[i].isReady) 
                    return false;       

            return true;
        
         }

    }

	public Text currentSelectionName;
	public Image currentSelectionDisplay;

	[HideInInspector]
	public Animator anim;

    private CursorBehaviour cursor;
    public bool hasMadeSelection = false;
    public bool hasJoinedTheGame = false;
	private Characters currentCharacter;
    private int skinIndex;
    private int colorIndex;
    private Teams team;
	private InputContext _inputContext;

    void Start() {
		
        playerBoxes[playerId] = this;
		_inputContext = Contexts.sharedInstance.input;

    }

    void Update() {

        if (cursor == null) {
            cursor = CursorBehaviour.allCursors[listener.playerId];
            ToggleVisibility(hasJoinedTheGame);
        }

        if (!hasMadeSelection && cursor.currentTarget != null) {

			Characters currentCharacter = cursor.currentTarget.GetComponent <CharacterBox> ().character;

			currentSelectionDisplay.sprite = Assets.Get (currentCharacter).uiInfo.bigSprite;
			currentSelectionDisplay.color = new Color (1f, 1f, 1f, 0.5f);

            CharacterBlueprint blueprint = Assets.Get(currentCharacter);
            currentSelectionName.text = blueprint.name;

        }

    }

    void UpdateEntitas() {

		foreach (InputEntity e in _inputContext.GetEntitiesWithControllerID (playerId)) {

			e.ReplacePlayerData (new PlayerData((Characters)currentCharacter, team, skinIndex, colorIndex));

		}

    }

    public override void OnMove(Vector2 input) {
        
        if(!hasJoinedTheGame && input != Vector2.zero)
            JoinTheGame();

    }

    public override void OnAButton() {

        if (!hasJoinedTheGame)
            JoinTheGame();
        else if(!hasMadeSelection)
            SelectCharacter();
        
    }

    public override void OnBButton() {

        if(hasMadeSelection)
            DeselectCharacter();
        else if(hasJoinedTheGame)
            LeaveTheGame();

    }

	public override void OnRButton() {

		print ("R");

	}

	public override void OnLButton() {

		print ("L");

	}

	public override void OnXButton() {
		colorIndex++;

		print (colorIndex);

	}

	public override void OnYButton() {
		colorIndex--;

		print (colorIndex);

	}

	public override void OnStartButton() {

		print ("Start");

	}

    void SelectCharacter() {
        currentCharacter = cursor.currentTarget.GetComponent<CharacterBox>().character;
		CharacterBlueprint blueprint = Assets.Get (currentCharacter);

		currentSelectionName.text = blueprint.name;
		currentSelectionDisplay.sprite = blueprint.uiInfo.bigSprite;
        currentSelectionDisplay.color = new Color(1f, 1f, 1f, 1f);
        hasMadeSelection = true;

        UpdateEntitas(); //will be moved to when you 'Press Start'
    }

    void DeselectCharacter() {
        currentSelectionName.text = "";
        currentSelectionDisplay.color = new Color(1f, 1f, 1f, 0f);

        hasMadeSelection = false;
    }

    void JoinTheGame() {
        hasJoinedTheGame = true;

        ToggleVisibility(hasJoinedTheGame);
    }

    void LeaveTheGame() {
        hasJoinedTheGame = false;

        ToggleVisibility(hasJoinedTheGame);
    }

    void ToggleVisibility(bool _isVisible) {
		
        cursor.GetComponent<Image>().enabled = _isVisible;
        cursor.gameObject.ToggleChildren(_isVisible);

		GetComponent<Image>().color = new Color (0.5f, 0.5f, 0.5f, 1f);

        gameObject.ToggleChildren(_isVisible);

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
		playerId = i;
	}

	public int GetMaxObjects () {
		return GameConstants.MAX_PLAYERS;
	}

	public MonoBehaviour GetScript () {
		return this;
	}

	public Transform GetTransform () {
		return transform;
	}

}