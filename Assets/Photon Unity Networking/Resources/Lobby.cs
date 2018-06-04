using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;

public class Lobby : PunBehaviour {

    public string gameVersion = "1";
    public Text lobbyText;
    public GameObject StartGameButton;
    public byte minPlayers;
    public InputField nickNamefield;

    //get nickname from textField
    private string NickName() {
        return nickNamefield.text;
    }

    //open connection to photon servers
    public void Connect() {
        if (PhotonNetwork.connected) {
            PhotonNetwork.JoinRandomRoom();
        } else {
            PhotonNetwork.ConnectUsingSettings(gameVersion);
        }
    }

    //Check if client is connected to master server, try to join room and set player nickname
    public override void OnConnectedToMaster() {
        Debug.Log("Connected");
        PhotonNetwork.JoinRandomRoom();
        if (NickName() != "") {
            PhotonNetwork.player.NickName = NickName();
            //nickNamefield.gameObject.SetActive(false);
            Debug.Log(PhotonNetwork.player.NickName);
        }
    }

    //if there's no room available, create a new room and set name to amount of rooms
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg) {
        string newRoom = "Room: " + (PhotonNetwork.countOfRooms + 1).ToString();
        PhotonNetwork.CreateRoom(newRoom, new RoomOptions() { MaxPlayers = minPlayers }, null);
        Debug.Log("Created room: " + newRoom);
    }

    //if client entered room, call RPC on all clients to update player UI, and pass updated playerlist
    public override void OnJoinedRoom() {
        Debug.Log("Joined Room " + PhotonNetwork.room.Name);
        photonView.RPC("UpdateUI", PhotonTargets.AllBufferedViaServer, PhotonNetwork.playerList.Length);
        
    }

    //update lobby ui, check if min amount of players is reaced and shows updated list of players
    [PunRPC]
    public void UpdateUI(int _players) {
        lobbyText.text = "Players in lobby: " + _players.ToString();
        if (_players == PhotonNetwork.room.MaxPlayers) {
            StartGameButton.SetActive(true);
        }
        Debug.Log("Updated UI");
        for (int i = 0; i <PhotonNetwork.playerList.Length;i ++) {
            Debug.Log(PhotonNetwork.playerList[i].NickName);
        }
    }
}
