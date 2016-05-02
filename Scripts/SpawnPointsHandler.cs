using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPointsHandler : MonoBehaviour {

    public List<SpawnPoint> spawnPoints;
    public List<PlayerScript> players;
    //public int mPlayers;

    void Start()
    {
        print("SpawnPointhandler started");

        PlayerScript[] mPlayers = FindObjectsOfType<PlayerScript>();

        for (int i = 0; i < mPlayers.Length; i++)
        {
            players.Add(mPlayers[i]);
        }
        
        SpawnPoint[] spawnpoints2 = FindObjectsOfType<SpawnPoint>();
        for (int i = 0; i < spawnpoints2.Length; i++)
        {
            spawnPoints.Add(spawnpoints2[i]);
        }
    }

	public void setSpawnPoint(GameObject player)
    {
        int playerAmount = PhotonNetwork.playerList.Length;
        /*if (playerAmount <= spawnPoints.Count)
        {
            print("Playeramount is " + playerAmount);
            players.Add(player.GetComponent<PlayerScript>());
            spawnPoints[playerAmount - 1].occupy(player);
            player.transform.position = spawnPoints[playerAmount - 1].transform.position;
            player.GetComponent<PlayerScript>().setSpawnPoint(spawnPoints[playerAmount - 1].getVector());
            print("Spawns player at " + spawnPoints[playerAmount - 1].transform.position);
        }
        else
        {
            Debug.LogError("There are more players then spawnpoints... wtf?!");
        }*/


        // Testa med annan isspawnpointtaken. Alla spawnpoints behöver nog en photon view.
        /*for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (isSpawnPointTaken(i) == false)
            {
                player.GetComponent<PlayerScript>().spawnNumber = i;
                players.Add(player.GetComponent<PlayerScript>());
                spawnPoints[i].occupy(player);
                player.transform.position = spawnPoints[i].transform.position;
                player.GetComponent<PlayerScript>().setSpawnPoint(spawnPoints[i].getVector());
                print("Spawns player at " + spawnPoints[i].transform.position);
            }
            else
            {
                print("No empty spawnpoints");
            }
        }*/

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            print("Spawnpoint " + i + "'s status is " + spawnPoints[i].getStatus() + " or " + spawnPoints[i].mTaken + " or " + spawnPoints[i].isTaken());
            if (spawnPoints[i].isTaken() == false)
            {
                spawnPoints[i].occupy(player);
                player.transform.position = spawnPoints[i].transform.position;
                player.GetComponent<PlayerScript>().setSpawnPoint(spawnPoints[i].getVector());
                print("Spawns player at " + spawnPoints[i].transform.position);
                break;
            }
        }
    }

    bool isSpawnPointTaken(int point)
    {
        // Testa med PhotonNetwork.playerList istället för players
        PlayerScript[] players = FindObjectsOfType<PlayerScript>();

        for (int i = 0; i < players.Length; i++)
        {
            print(i);
            if (players[i].spawnNumber == point)
            {
                return true;
            }
        }

        return false;

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(spawnPoints);
        }
        else {
            spawnPoints = (List<SpawnPoint>)stream.ReceiveNext();
        }

    }
}
