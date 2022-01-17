using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Cat : MonoBehaviour
{
    [SerializeField]
    private GameObject mouse;
    [SerializeField]
    private GameObject decoy;

    private float speed = 0.4f;

    void FixedUpdate()
    {
        // Calculate distance between cat and mice
        float distToMouse = Vector3.Distance(this.gameObject.transform.position, mouse.transform.position);
        float distToDecoy = Vector3.Distance(this.gameObject.transform.position, decoy.transform.position);

        GameObject obj = (distToDecoy <= distToMouse) ? decoy : mouse; // Get closest mouse

        // Move cat towards  closest mouse
        Vector3 dir = obj.transform.position - this.gameObject.transform.position; // Calculate direction vector
        dir = dir.normalized; // Normalize resultant vector to unit Vector 
        this.gameObject.transform.position += dir * Time.deltaTime * speed; // Move in the direction of the direction vector every frame
    }
}
