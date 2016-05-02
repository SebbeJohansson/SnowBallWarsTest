using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : Function
{
    public int sceneIndex;

    public override void runFunction()
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
