using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : PunBehaviour {

    public string gameVersion = "1";
    public Text lobbyText;
    public Text connectionStatus;
    public Text[] playersOnline;
    public GameObject StartGameButton;
    public byte minPlayers;
    public InputField nickNamefield;
    public GameObject playerObject;

    //get nickname from textField
    private string NickName() {
        return nickNamefield.text;
    }
    private void Start() {
        connectionStatus.text = "Not connected";
    }

    //open connection to photon servers
    public void Connect() {
        //connectionStatus.text = "trying to Connect";
        PhotonNetwork.ConnectUsingSettings(gameVersion);
        connectionStatus.text = "looking for room";
    }

    //Check if client is connected to master server, try to join room and set player nickname
    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinRandomRoom();
        connectionStatus.text = "trying to join room";
        if (NickName() != "") {
            PhotonNetwork.player.NickName = NickName();
            nickNamefield.gameObject.SetActive(false);
        }
    }

    //if there's no room available, create a new room and set name to amount of rooms
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg) {
        connectionStatus.text = "No room available, attempting to create new room";
        string newRoom = "Room: " + (PhotonNetwork.countOfRooms + 1).ToString();
        PhotonNetwork.CreateRoom(newRoom, new RoomOptions() { MaxPlayers = minPlayers }, null);
    }

    //if client entered room, call RPC on all clients to update player UI, and pass updated playerlist
    public override void OnJoinedRoom() {
        connectionStatus.text = "sucesfully connected to room";
        photonView.RPC("UpdateUI", PhotonTargets.AllBufferedViaServer, PhotonNetwork.playerList.Length);
        CheckPlayers(PhotonNetwork.playerList.Length);
        PhotonNetwork.Instantiate(playerObject.name, new Vector3(0, 0, 0), Quaternion.identity, 0);

    }

    //update lobby ui, check if min amount of players is reaced and shows updated list of players
    [PunRPC]
    public void UpdateUI(int _players) {
        lobbyText.text = "Players in lobby: " + _players.ToString();

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
            playersOnline[i].text = PhotonNetwork.playerList[i].NickName;
        }
    }

    public void CheckPlayers(int _players) {
        if (_players == PhotonNetwork.room.MaxPlayers) {
            photonView.RPC("ToGame", PhotonTargets.AllBufferedViaServer, null);
        }
    }

    [PunRPC]
    public void ToGame() {
        PhotonNetwork.LoadLevel(1);
       // StartGameButton.SetActive(true);
    }
}
