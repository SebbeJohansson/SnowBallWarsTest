using UnityEngine;
using System.Collections;

public class SnowBallScript : MonoBehaviour {

    GameObject mOwner;

    SnowballSpawner sbs;

	void Awake()
    {
        sbs = FindObjectOfType<SnowballSpawner>();
    }

    void FixedUpdate()
    {
        if(transform.position.y < -50)
        {
            print("snowball is under -50");
            DestroyImmediate(gameObject);
            if (sbs != null)
            {
                sbs.SpawnSnowball();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Boundry")
        {
            GetComponent<Collider>().isTrigger = true;
        }
        else if (collider.gameObject.tag == "Snowball")
        {
            Destroy(gameObject);
            Destroy(collider.gameObject);
        }
    }
    
    public void setOwner(GameObject input)
    {
        mOwner = input;
        print("New owner is " + mOwner);
    }

    public GameObject getOwner()
    {
        return mOwner;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            //stream.SendNext(ints);
        }
        else {
            transform.position = (Vector3)stream.ReceiveNext();
            //ints = (int)stream.ReceiveNext();
        }

    }

}
