using UnityEngine;
using System.Collections;
using Photon;

public class PlayerScript : Photon.MonoBehaviour {

    ScoreBoardScript mSB;
    public GameObject snowBall;
    public bool ultimatePower = false;
    public int spawnNumber;

    Vector3 spawnPoint;
    bool isInPile = false;

    bool holdingDefence = false;
    GameObject currentDefence;

    void Awake()
    {
        mSB = FindObjectOfType<ScoreBoardScript>();
        spawnPoint = transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            GameObject newSnowBall;
            //newSnowBall = PhotonNetwork.InstantiateSceneObject("snowball", transform.position, transform.rotation, 0, null);
            newSnowBall = PhotonNetwork.Instantiate("snowball", transform.position, transform.rotation, 0, null);
            newSnowBall.name = "Snowball";
            newSnowBall.GetComponent<SnowBallScript>().setOwner(gameObject);
        }

        if (ultimatePower == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject newSnowBall;
                Rigidbody rigid;
                //newSnowBall = PhotonNetwork.InstantiateSceneObject("snowball", transform.position, transform.rotation, 0, null);
                newSnowBall = PhotonNetwork.Instantiate("snowball", transform.position, transform.rotation, 0, null);
                newSnowBall.name = "Snowball";
                newSnowBall.GetComponent<SnowBallScript>().setOwner(gameObject);
                newSnowBall.transform.position = transform.position + transform.GetComponentInChildren<Camera>().transform.forward;

                rigid = newSnowBall.GetComponent<Rigidbody>();
                rigid.AddForce(transform.GetComponentInChildren<Camera>().transform.forward * (1000));


            }
        }

        if (isInPile)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);

                if (hit.point != new Vector3(0, 0, 0) && hit.transform.tag == "Snowpile")
                {
                    GameObject newSnowBall;
                    //newSnowBall = PhotonNetwork.InstantiateSceneObject("snowball", transform.position, transform.rotation, 0, null);
                    newSnowBall = PhotonNetwork.Instantiate("snowball", transform.position, transform.rotation, 0, null);
                    newSnowBall.name = "Snowball";
                    newSnowBall.GetComponent<SnowBallScript>().setOwner(gameObject);
                    newSnowBall.transform.position = hit.point;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            print(hit.transform.tag);
            print(hit.transform.gameObject);

            if (hit.transform.tag == "Defence")
            {
                print("Holding defence");
                holdingDefence = true;
                currentDefence = hit.transform.gameObject;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            print("Dropped defence");
            holdingDefence = false;
            if (currentDefence != null)
            {
                currentDefence.GetComponent<Rigidbody>().isKinematic = false;
                currentDefence.GetComponent<Collider>().isTrigger = true;
            }
        }

        if (holdingDefence == true)
        {
            if (currentDefence != null)
            {
                currentDefence.transform.position = transform.position + (transform.GetComponentInChildren<Camera>().transform.forward * 2) + new Vector3(0,1f,0);
                
            }
            else
            {
                Debug.LogWarning("This should not have happend. For some reason holdingDefence was true but we didnt have a current defence object.");
            }
        }
        else
        {
            if (currentDefence != null)
            {
                if (currentDefence.transform.position.y <= 0)
                {
                    //currentDefence.transform.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        
    }

    void OnTriggerEnter(Collider collider)
    {
        isInPile = true;
        if (collider.gameObject.tag == "Snowball")
        {
            if (collider.gameObject.GetComponent<SnowBallScript>().getOwner() != gameObject)
            {
                Destroy(collider.gameObject);
                print("YOU GOT HIT!");
                if (mSB != null)
                {
                    mSB.alterScore(-1);
                }
            }
        }
        else if (collider.gameObject.tag == "Boundry")
        {
            //transform.position = spawnPoint;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        isInPile = false;
    }

    public void setSpawnPoint(Vector3 spawnPosition)
    {
        spawnPoint = spawnPosition;
    }

    public Vector3 getSpawnPoint()
    {
        return spawnPoint;
    }
}
