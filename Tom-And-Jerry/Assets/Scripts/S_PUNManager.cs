using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PUNManager : MonoBehaviour
{
    public static S_PUNManager punManager;

    // Start is called before the first frame update
    void Start()
    {
        if (punManager == null)
        {
            punManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
