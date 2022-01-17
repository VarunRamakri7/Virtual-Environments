using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject mouse;
    [SerializeField]
    private GameObject decoy;
    [SerializeField]
    private GameObject cat;

    private void OnTriggerEnter(Collider other)
    {
        // End Game
        if (other.gameObject.tag == "mouse")
        {
            other.GetComponent<S_Mice>().CanMove = false; // Disable movement

            Time.timeScale = 0; // Pause Game
            canvas.SetActive(true); // Show game end UI
        }
    }

    public void Replay()
    {
        // Reset positions
        mouse.transform.position = mouse.GetComponent<S_Mice>().StartPos;
        decoy.transform.position = decoy.GetComponent<S_Mice>().StartPos;
        cat.transform.position = cat.GetComponent<S_Cat>().StartPos;

        mouse.GetComponent<S_Mice>().CanMove = true; // Enable movement

        Time.timeScale = 1; // Unpause Game
    }
}
