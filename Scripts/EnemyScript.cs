using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    ScoreBoardScript mSB;

    void Start()
    {
        mSB = FindObjectOfType<ScoreBoardScript>();
    }


    void OnTriggerEnter(Collider collider)
    {
        if (this.enabled == true)
        {
            if (collider.gameObject.tag == "Snowball")
            {
                Destroy(collider.gameObject);
                print("YOU HIT HIM!");
                mSB.alterScore(1);
            }
        }
    }

}
