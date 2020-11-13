using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject connectedWindow;
    [SerializeField] private GameObject disconnectedWindow;

    void Start()
    {
    }

    public void OnClick_ConnectedBtn()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting");
    }

    // public void OnConnectedToMaster()
    // {
    //     PhotonNetwork.JoinLobby(TypedLobby.Default);
    // }

    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectedWindow.gameObject.SetActive(true);
    }

    public override void OnConnected()
    {
        Debug.Log("joined lobby");
        if(disconnectedWindow.activeSelf)
            disconnectedWindow.SetActive(false);

        connectedWindow.SetActive(true);
    }
}
