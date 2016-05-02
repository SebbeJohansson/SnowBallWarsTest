using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {

    bool controllerInstatiated = false;

    public bool gameStarted = false;
    public bool gameEnded = false;

	public GameObject gameGUI;
	private PositionLerp posLerp;

    public int ints = 0;

    Room room;

    void Start()
    {
		posLerp = gameGUI.GetComponent<PositionLerp>();
        print("Match controller started");
		/*if (PhotonNetwork.playerList.Length >= room.maxPlayers)
		{
			gameStarted = true;
			print("Is Game Started? - " + gameStarted);
			// vad som ska hända när det blir fullt. Alltså ska guin försvinna.
		}*/
    }

    void FixedUpdate()
    {
		if (controllerInstatiated == true)
        {
            if (gameStarted == false)
            {
                if (PhotonNetwork.playerList.Length >= room.maxPlayers)
                {
					gameStarted = true;
					print("Is Game Started? - " + gameStarted);
					// vad som ska hända när det blir fullt. Alltså ska guin försvinna.
					posLerp.showObject(false);
                }
            }
            else
            {
                if (PhotonNetwork.playerList.Length < room.maxPlayers)
                {
                    // vad som ska hända när någon spelare har lämnat. WALKOVER! WINNNNNNER!
					posLerp.showObject(true);
                }
            }
        }
    }

    public void InitiateController()
    {
        print("Controller instatiated");
        controllerInstatiated = true;
        room = PhotonNetwork.room;
        print(PhotonNetwork.playerList.Length);
        print(room.maxPlayers);

        ints++;
        if (PhotonNetwork.playerList.Length >= room.maxPlayers)
        {
            gameStarted = true;
			print("Is Game Started? - " + gameStarted);
            // vad som ska hända när controllern startar och det är fullt. Alltså ska guin försvinna.
			posLerp.showObject(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameStarted);
            stream.SendNext(gameEnded);
            stream.SendNext(ints);
        }
        else {
            gameStarted = (bool)stream.ReceiveNext();
            gameEnded = (bool)stream.ReceiveNext();
            ints = (int)stream.ReceiveNext();
        }

    }


}
