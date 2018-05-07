using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private Player player1, player2;
    public static Player currentPlayer;

    public Text scoreUI;
    public Text turnUI;

    public int turn = 0;
    public int player1Score, player2Score;


    private void Start()
    {
        currentPlayer = player1;
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void SetTurn()
    {
        turn++;
        if (currentPlayer == player1)
        {
            currentPlayer = player2;
        }
        else if (currentPlayer == player2)
        {
            currentPlayer = player1;
        }
    }

    //updates turn and score UI when called
    private void UpdateUI()
    {
        turnUI.text = "Turn: " + turn.ToString();
        scoreUI.text = "Now playing: " + currentPlayer.nickName + " Score: " + currentPlayer.score;

    }

}
