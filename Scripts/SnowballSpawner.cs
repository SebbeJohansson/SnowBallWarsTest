using UnityEngine;
using System.Collections;

public class SnowballSpawner : MonoBehaviour {
    Object snowBallPrefab;

    void Start()
    {
        snowBallPrefab = Resources.Load("snowball");
    }

    public void SpawnSnowball()
    {
        if (snowBallPrefab != null)
        {
            GameObject snowball = (GameObject)Instantiate(snowBallPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Prefab is not set on " + gameObject.name);
        }
    }

}
