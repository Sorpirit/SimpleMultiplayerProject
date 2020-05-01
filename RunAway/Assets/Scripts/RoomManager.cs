using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text logLabel;
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        Vector2 randPos = new Vector2(Random.Range(-5f,5f),Random.Range(-5f,5f));
        PhotonNetwork.Instantiate(playerPrefab.name,randPos,Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Log("Joined " + newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Log("Leave " + otherPlayer.NickName);
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    private void Log(string text)
    {
        Debug.Log(text);
        logLabel.text += text + "\n";
    }
}
