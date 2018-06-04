using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class ScoreManager : PunBehaviour{ 
    public Text[] playerTexts;
    private PhotonPlayer[] players;

    private void Start() {
        photonView.RPC("SetNames", PhotonTargets.AllBufferedViaServer, null);
    }

    [PunRPC]
    public void SetNames() {
        players = PhotonNetwork.playerList;
        for (int i = 0; i < players.Length; i++) {
            playerTexts[i].text = players[i].NickName;
        }
    }

}
