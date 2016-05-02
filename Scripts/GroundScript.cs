using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

    GameObject mainCamera;

    SnowballSpawner sbs;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        sbs = FindObjectOfType<SnowballSpawner>();
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Snowball")
        {

            GameObject snowBall = collider.gameObject;
            Vector3 velocity = snowBall.GetComponent<Rigidbody>().velocity;
            print("Snowball hit the ground and merged with it.");
            
            Destroy(snowBall);
            if (sbs != null)
            {
                sbs.SpawnSnowball();
            }
        }
        else if (collider.tag == "Defence")
        {
            collider.GetComponent<Collider>().isTrigger = false;
            if (collider.GetComponent<Rigidbody>().isKinematic != true)
            {
                collider.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        /*
        if (velocity.y > 8)
        {

            if (snowBall.GetComponentInChildren<Camera>().gameObject != null)
            {
                if (snowBall.GetComponentInChildren<Camera>().gameObject == mainCamera)
                {
                    print("Yes snowball has main camera");
                    mainCamera.transform.parent = transform.root;
                }
            }


        }
        else
        {
            collider.isTrigger = false;
            collider.gameObject.transform.position = new Vector3(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 10, collider.gameObject.transform.position.z);
        }
        */

    }

}
