using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class S_PUNManager : MonoBehaviourPunCallbacks
{
    public static S_PUNManager punManager;

    [Tooltip("The maximum number of players per room")]
    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    private string gameVersion = "1";
    private bool isConnecting;
    private const string roomName = "tom-and-jerry-room";

    void Awake()
    {
        // Makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (punManager == null)
        {
            punManager = this;
        }

        // Connect();

        // Create room for game
        // PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            //PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.GameVersion = gameVersion;
        }

    }

#region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");

        if (isConnecting)
        {
            // PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);

        isConnecting = false;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom() was called by PUN");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreatedRoomFailed() called by PUN.");

        Debug.Log("Message: " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN.");
        Debug.Log("Joined PlayerCount: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinedRoomFailed() called by PUN.");

        Debug.Log("Message: " + message);

        // Create room for game
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    #endregion
}
