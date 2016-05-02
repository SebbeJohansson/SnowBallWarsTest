using UnityEngine;
using System.Collections;

public class MenuObject : MonoBehaviour {
    
    public Function[] functions;

    void Awake()
    {

    }

    void RunFunctions()
    {
        for (int i = 0; i < functions.Length; i++)
        {
            functions[i].runFunction();
        }
    }

    void OnTriggerEnter()
    {
        RunFunctions();
    }


}
