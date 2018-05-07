using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int playerId;
    public string nickName;
    public int score;
    
    public Player(int _playerId, string _nickName, int _score)
    {
        playerId = _playerId;
        nickName = _nickName;
        score = _score;
    }
}
