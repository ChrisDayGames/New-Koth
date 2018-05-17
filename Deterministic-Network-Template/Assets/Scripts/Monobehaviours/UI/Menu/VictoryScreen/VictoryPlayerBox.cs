using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPlayerBox : InputEventPlanner, IGeneratable {

    public static VictoryPlayerBox[] playerBoxes = new VictoryPlayerBox[GameConstants.MAX_PLAYERS];

    public int playerId {

        get { return listener.playerId; }
        set { if (value >= 0 && value < GameConstants.MAX_PLAYERS) listener.playerId = value; }

    }

    public Image buttonImage;
    public Image badgeImage;
    public Text badgeTitle;

    private bool ready = false;
    public bool isReady { get { return ready; } }

    private InputContext _inputContext;

    public static bool allPlayersReady {

        get {

            for (int i = 0; i < GameConstants.MAX_PLAYERS; i++)
                if (playerBoxes[i] != null && !playerBoxes[i].isReady)
                    return false;

            return true;

        }

    }

    void Start() {

        playerBoxes[playerId] = this;
        _inputContext = Contexts.sharedInstance.input;

        ready = false;
        buttonImage.color = Color.red;
        buttonImage.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    private void ReadyPlayer() {
        ready = true;
        buttonImage.color = Color.green;
        buttonImage.GetComponent<RectTransform>().localScale = Vector3.one * 0.8f;
    }
    
    public override void OnAButton() {

        if (!isReady) {
            
            ReadyPlayer();
            
            if(allPlayersReady) {
                GetComponentInParent<LinkBehaviour>().ClickFunction();
            }

        }
         
    }
    
    private void AssignBadge() {
        
    }

    public void Generate(int i) {
        playerId = i;
        AssignBadge();
    }

    public int GetMaxObjects() {
        return GameConstants.MAX_PLAYERS;
    }

    public MonoBehaviour GetScript() {
        return this;
    }

    public Transform GetTransform() {
        return transform;
    }
}
