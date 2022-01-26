using UnityEngine;

public class S_Mice : MonoBehaviour
{
    [SerializeField]
    private S_PUNManager punManager;
    [SerializeField]
    private S_GameManager gameManager;

    private bool isDecoy;
    public bool IsDecoy { get; set; }

    private Vector3 startPos;
    public Vector3 StartPos { get; set; }

    private bool canMove;

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
        canMove = true; // Enable movement

        if (gameManager.isMultiplayer)
        {
            punManager.Connect(); // Connect to Network
        }
    }

    private void Update()
    {
        // Move mouse with keyboard press
        if (canMove && (!isDecoy || gameManager.isMultiplayer))
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
            other.gameObject.transform.position = other.gameObject.GetComponent<S_Cat>().StartPos; // Move cat
            this.gameObject.transform.position = startPos; // Move decoy
        }
        else if (!isDecoy && other.gameObject.tag == "cheese")
        {
            canMove = false;
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
        if (!gameManager.isMultiplayer && isDecoy)
        {
            float distance_to_screen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position).z;
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            this.gameObject.transform.position = new Vector3(pos_move.x, transform.position.y, pos_move.z);
        }
    }

    public void SetMovementStatus(bool status)
    {
        canMove = status;
    }

    #endregion
}
