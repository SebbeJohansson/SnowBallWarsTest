using UnityEngine;
using System.Collections;
using Photon;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkController : Photon.PunBehaviour
{

    public double version = 0.1;
    MatchController mC;

    SpawnPointsHandler spHandler;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(version.ToString());
        spHandler = FindObjectOfType<SpawnPointsHandler>();
        mC = FindObjectOfType<MatchController>();
        //PhotonNetwork.logLevel = PhotonLogLevel.Full;
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connecting to Master.");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created a room");
        mC.InitiateController();
        /*PhotonNetwork.InstantiateSceneObject("SpawnPoints", transform.position, transform.rotation, 0, null);
        spHandler = FindObjectOfType<SpawnPointsHandler>();*/
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined a lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("Cant join random room");
        // Creates a new room with 2 player max. for 1v1 duals.
        RoomOptions roomOptions = new RoomOptions() { maxPlayers = 2 };
        PhotonNetwork.CreateRoom(null, roomOptions, null);

        //PhotonNetwork.InstantiateSceneObject("SpawnPoints", transform.position, transform.rotation, 0, null);
    }

    public override void OnJoinedRoom()
    {
        //print(PhotonNetwork.playerList.Length);
        Vector3 spawnpos = new Vector3(-0.6985493f, 1.917018f, -1.207728f);
        GameObject player = PhotonNetwork.Instantiate("player", spawnpos, Quaternion.identity, 0);
        int mPlayers = FindObjectsOfType<PlayerScript>().Length;
        //print(mPlayers);
        player.SetActive(true);
        //print(spHandler);
        spHandler.setSpawnPoint(player);
        player.transform.position = player.GetComponent<PlayerScript>().getSpawnPoint();

        FirstPersonController controller = player.GetComponent<FirstPersonController>();
        controller.enabled = true;

        Camera playerCamera = player.GetComponentInChildren<Camera>();
        playerCamera.enabled = true;

        AudioListener listener = player.GetComponentInChildren<AudioListener>();
        listener.enabled = true;

        EnemyScript enemy = player.GetComponent<EnemyScript>();
        enemy.enabled = false;

    }
    

}
