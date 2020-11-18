using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class UIHandler : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomTF;
    public TMP_InputField joinRoomTF;
    [SerializeField] private Manager manager;

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomTF.text, null);
        Debug.Log("Try to open level 1");
        manager.CreateSecondPlayer();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join room successfully");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Connection was failed " + returnCode + " Message " + message);
    }

    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions {MaxPlayers = 4}, null);
        manager.CreateFirstPlayer();
    }
}