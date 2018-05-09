using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private Player player1, player2;
    public static Player currentPlayer;
    public static Player waitingPlayer;

    public GameObject[] noosePlayer1, noosePlayer2;
    public GameObject noose1, noose2;
    public Text activeScoreUI;
    public Text waitingScoreUI;
    public Text turnUI;

    public int maxWrongAnswers;
    public int turn = 0;
    public int player1Score, player2Score;


    private void Awake()
    {
        currentPlayer = player1;
        waitingPlayer = player2;
        GetNooseComponents(noose1, noosePlayer1);
        GetNooseComponents(noose2, noosePlayer2);
        UpdateUI();
    }

    //puts the components of the gallow in an array
    private void GetNooseComponents(GameObject noose, GameObject[] nooseArray)
    {
        // noose.transform.childCount 
        for (int i = 0; i < noose.transform.childCount; i++)
        {
            nooseArray[i] = noose.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        UpdateUI();

        if (player1.wrongAnswers >= maxWrongAnswers || player2.wrongAnswers >= maxWrongAnswers)
        {
            GameOver();
        }
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
        activeScoreUI.text = "Player 1: " + currentPlayer.nickName + " Score: " + currentPlayer.score;
        waitingScoreUI.text = "Player 2: " + waitingPlayer.nickName + " Score: " + waitingPlayer.score;
    }


    //game over screen
    void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    public void UpdateNoose()
    {

        int wronganswers = currentPlayer.wrongAnswers;

        if (wronganswers > 0)
        {
            if (currentPlayer == player1)
            {
                noosePlayer1[player1.wrongAnswers -1].SetActive(true);
            }

            if (currentPlayer == player2)
            {
                noosePlayer2[player2.wrongAnswers-1].SetActive(true);
            }
        }
    }
}
