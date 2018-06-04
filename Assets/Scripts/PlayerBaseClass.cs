using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        this.name = PhotonNetwork.player.NickName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
