using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CConnectManager : Photon.PunBehaviour {

    GameObject _startPanel;
    Text _connectMsg;
    InputField _nameInput;
    CGameManager _gameManager;

    Button _join;

    private void Awake()
    {
        _startPanel = GameObject.Find("StartPanel");
        _connectMsg = GameObject.Find("ConnectMsg").GetComponent<Text>();
        _nameInput = GameObject.Find("NameInputField").GetComponent<InputField>(); 
        _gameManager = FindObjectOfType<CGameManager>();
        _join = GameObject.Find("JoinButton").GetComponent<Button>();




        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.ConnectUsingSettings("v1.0"))
            {
                _connectMsg.text = "Attempting to connect server...";
            }
            else
            {
                _connectMsg.text = "Failed to connect server";
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _join.interactable = PhotonNetwork.connected;
	}

    public void OnRoomJoinButtonClick()
    {
        PhotonNetwork.JoinOrCreateRoom(
            "GameRoom",
            new RoomOptions()
            {
                MaxPlayers = 20,
                IsOpen = true,
                IsVisible = true
            },
            TypedLobby.Default
        );
    }


    public override void OnConnectedToPhoton()
    {
        base.OnConnectedToPhoton();
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        base.OnConnectionFail(cause);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
    }

    public override void OnJoinedLobby()
    {
        _connectMsg.text = "Connected to Lobby";
    }

    public override void OnJoinedRoom()
    {
        if (_nameInput.text.Length > 0)
        {
            PhotonNetwork.playerName = _nameInput.text;
        }
        else
        {
            PhotonNetwork.playerName = "dummy";
            /*RandomNameGenerator.NameGenerator.GenerateLastName();*/
            //make random name
        }

        _startPanel.SetActive(false);
        _gameManager.CreateNetPlayer();

        string roomName = PhotonNetwork.room.Name;
        _connectMsg.text = "Connected to room : " + roomName;

    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        base.OnPhotonCreateRoomFailed(codeAndMsg);
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        base.OnPhotonJoinRoomFailed(codeAndMsg);
    }

}
