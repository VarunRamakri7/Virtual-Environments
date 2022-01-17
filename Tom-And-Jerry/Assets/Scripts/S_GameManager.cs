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
            // Disable movement
            mouse.GetComponent<S_Mice>().CanMove = false;
            decoy.GetComponent<S_Mice>().CanMove = false;

            canvas.SetActive(true); // Show game end UI

            Time.timeScale = 0; // Pause Game
        }
    }

    public void Replay()
    {
        Time.timeScale = 1; // Unpause Game

        // Enable movement
        mouse.GetComponent<S_Mice>().CanMove = true;
        decoy.GetComponent<S_Mice>().CanMove = true;

        // Reset positions
        mouse.transform.position = mouse.GetComponent<S_Mice>().StartPos;
        decoy.transform.position = decoy.GetComponent<S_Mice>().StartPos;
        cat.transform.position = cat.GetComponent<S_Cat>().StartPos;

    }
}
