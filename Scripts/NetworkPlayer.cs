using UnityEngine;
using System.Collections;
using Photon;

public class NetworkPlayer : Photon.MonoBehaviour {

    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

    bool mOnlineStatus = false;

    void Start()
    {
        mOnlineStatus = FindObjectOfType<NetworkController>().enabled;
    }

    void Update()
    {
        if (mOnlineStatus)
        {
            if (!photonView.isMine)
            {
                transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
                //transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (mOnlineStatus)
        {
            if (stream.isWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(transform.position);

                //stream.SendNext(transform.rotation);
                print("Sending transform");
            }
            else
            {
                // Network player, receive data
                this.correctPlayerPos = (Vector3)stream.ReceiveNext();
                //this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}
