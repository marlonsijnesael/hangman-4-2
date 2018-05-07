using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int playerId;
    public string nickName;
    public int score;
    public int wrongAnswers;

    public Player(int _playerId, string _nickName, int _score, int _wrongAnswers)
    {
        playerId = _playerId;
        nickName = _nickName;
        score = _score;
        wrongAnswers = _wrongAnswers;
    }
}
