using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

public class TurnManager : PunBehaviour {
    [SerializeField]
    public Player[] players = new Player[2];
    public static string currentPlayer;
    public static string waitingPlayer;
    public static GameObject CurrentplayerObject;

    public GameObject[] noosePlayer1, noosePlayer2;
    public GameObject noose1, noose2;
    public Text activeScoreUI;
    public Text waitingScoreUI;
    public Text turnUI;

    public int maxWrongAnswers;
    public int turn = 0;
    public int player1Score, player2Score;


    private void Start() {

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
            PhotonPlayer newpPlayer = PhotonNetwork.playerList[i];
            players[i] = new Player(newpPlayer.ID, newpPlayer.NickName, 0, 0);
        }

        currentPlayer = players[0].nickName;
        waitingPlayer = players[1].nickName;

        GetNooseComponents(noose1, noosePlayer1);
        GetNooseComponents(noose2, noosePlayer2);
        UpdateUI();
    }

    //puts the components of the gallow in an array
    private void GetNooseComponents(GameObject noose, GameObject[] nooseArray) {
        // noose.transform.childCount 
        for (int i = 0; i < noose.transform.childCount; i++) {
            nooseArray[i] = noose.transform.GetChild(i).gameObject;
        }
    }

    private void Update() {
        UpdateUI();

        if (players[0].wrongAnswers >= maxWrongAnswers || players[1].wrongAnswers >= maxWrongAnswers) {
            GameOver();
        }
    }

    //used in textfield to call the setturn on end edit
    public void SetTurnOnEdit() {
        photonView.RPC("SetTurn", PhotonTargets.AllBufferedViaServer, currentPlayer, waitingPlayer);
    }

    [PunRPC]
    private void SetTurn(string _current, string _waiting) {
        turn++;
        currentPlayer = _waiting;
        waitingPlayer = _current;
      
    }

    //updates turn and score UI when called
    private void UpdateUI() {
        turnUI.text = "Turn: " + turn.ToString();
    }

    //game over screen
    void GameOver() {
        SceneManager.LoadScene(2);
    }

    public void UpdateNoose() {



        if (currentPlayer == players[0].nickName) {
            if (players[0].wrongAnswers > 0) {
                noosePlayer1[players[0].wrongAnswers - 1].SetActive(true);
            }
        }

        if (currentPlayer == players[1].nickName) {
            if (players[1].wrongAnswers > 0) {
                noosePlayer1[players[1].wrongAnswers - 1].SetActive(true);
            }
        }
    }
}

