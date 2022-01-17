using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Mice : MonoBehaviour
{
    private bool isDecoy { get; set; }

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        // Check if this is the player mouse of decoy mouse
        if (this.gameObject.tag == "decoy")
        {
            isDecoy = true; // Mark decoy

            this.gameObject.GetComponent<Renderer>().material.color = Color.blue; // Change color of decoy
        }

        startPos = this.gameObject.transform.position; // Get starting tranform
    }

    private void Update()
    {
        /// Move mouse with keyboard press
        if (this.gameObject.tag == "mouse")
        {
            MoveMouse();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collide with Cheese or Cat
        if (other.gameObject.tag == "cat")
        {
            // Move to start location
            this.gameObject.transform.position = startPos;
        }
    }

#region Mouse Movement
    // Move mouse for keyboard press
    private void MoveMouse()
    {
        Vector3 position = this.transform.position;

        if (Input.GetKeyDown(KeyCode.A))
        {
            position.x--;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            position.x++;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            position.z++;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            position.z--;
        }

        this.transform.position = position;
    }

    // Move decoy on Mouse drag
    void OnMouseDrag()
    {
        if (this.gameObject.tag == "decoy")
        {
            float distance_to_screen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position).z;
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            this.gameObject.transform.position = new Vector3(pos_move.x, transform.position.y, pos_move.z);
        }
    }

    #endregion
}
