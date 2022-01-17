using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Mice : MonoBehaviour
{
    private bool isDecoy { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "decoy")
        {
            isDecoy = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collide with Cheese or Cat
    }
}
