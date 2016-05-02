using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    
    public bool mTaken = false;

    public GameObject occupant = null;

    public void occupy(GameObject player)
    {
        mTaken = true;
        occupant = player;
    }

    public bool isTaken()
    {
        if (occupant == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool getStatus()
    {
        return mTaken;
    }

    public Vector3 getVector()
    {
        return transform.position;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(occupant);
        }
        else {
            occupant = (GameObject)stream.ReceiveNext();
        }

    }

}
