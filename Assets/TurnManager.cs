using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private Player player1, player2;
    public static Player currentPlayer;
    public static Player waitingPlayer;

    public Text activeScoreUI;
    public Text waitingScoreUI;
    public Text turnUI;

    public int turn = 0;
    public int player1Score, player2Score;


    private void Start()
    {
        currentPlayer = player1;
        waitingPlayer = player2;
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    //used in textfield to call the setturn on end edit
    public void SetTurnOnEdit()
    {
        SetTurn(currentPlayer, waitingPlayer);
    }

    private void SetTurn(Player _current , Player _waiting)
    {
        turn++;
        currentPlayer = _waiting;
        waitingPlayer = _current;
    }

    //updates turn and score UI when called
    private void UpdateUI()
    {
        turnUI.text = "Turn: " + turn.ToString();
        activeScoreUI.text = "Now playing: " + currentPlayer.nickName + " Score: " + currentPlayer.score;
        waitingScoreUI.text = waitingPlayer.nickName + " Score: " + waitingPlayer.score;

    }

}
