using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text logLabel;
    
    private string _name;
    private string _gameVersion;
    private bool connected;
    
    
    private void Start()
    {
        _name = "Player" + Random.Range(1000, 10000);
        _gameVersion = "Pancake.1";
        
        Log(_name);
        
        PhotonNetwork.NickName = _name;

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = _gameVersion;
        connected = PhotonNetwork.ConnectUsingSettings();
        Log("is Connecting :" + connected);
    }

    public override void OnConnectedToMaster()
    {
        Log("OK...Connected!!!");
    }

    public override void OnJoinedRoom()
    {
        Log("Loading new scene...");

        SceneManager.LoadScene("Gameplay");
        //PhotonNetwork.LoadLevel("Gameplay");
    }

    public void CreateRoom(int maxPlayers)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = (byte) maxPlayers});
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    private void Log(string text)
    {
        Debug.Log(text);
        logLabel.text += text + "\n";
    }
}
