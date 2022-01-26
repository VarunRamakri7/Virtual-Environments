using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class S_GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject mouse;
    [SerializeField]
    private GameObject decoy;
    [SerializeField]
    private GameObject cat;
    [SerializeField]
    private GameObject waitBanner;

    public bool isMultiplayer = false;
    public bool canPlay = false;

    private void Start()
    {
        if (!isMultiplayer)
        {
            canPlay = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // End Game
        if (other.gameObject.tag == "mouse")
        {
            // Disable movement
            mouse.GetComponent<S_Mice>().SetMovementStatus(false);
            decoy.GetComponent<S_Mice>().SetMovementStatus(false);

            canvas.SetActive(true); // Show game end UI

            Time.timeScale = 0; // Pause Game
        }
    }

    public void Replay()
    {
        Time.timeScale = 1; // Unpause Game

        // Enable movement
        mouse.GetComponent<S_Mice>().SetMovementStatus(true);
        decoy.GetComponent<S_Mice>().SetMovementStatus(true);

        // Reset positions
        mouse.transform.position = mouse.GetComponent<S_Mice>().StartPos;
        decoy.transform.position = decoy.GetComponent<S_Mice>().StartPos;
        cat.transform.position = cat.GetComponent<S_Cat>().StartPos;

    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to start but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : PlayerCount", PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            waitBanner.SetActive(false); // Hide banner
            canPlay = true;
        }
    }

    #region Photon Callbacks

    public override void OnLeftRoom()
    {
        Debug.Log("Player left room");
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }

#endregion
}
