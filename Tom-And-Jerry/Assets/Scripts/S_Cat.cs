using UnityEngine;

public class S_Cat : MonoBehaviour
{
    [SerializeField]
    private GameObject mouse;
    [SerializeField]
    private GameObject decoy;
    [SerializeField]
    private S_GameManager gameManager;

    private Vector3 startPos;
    public Vector3 StartPos { get; set; }

    private float speed = 0.4f;

    private void Start()
    {
        startPos = this.gameObject.transform.position; // Get starting tranform
    }

    void FixedUpdate()
    {
        if (gameManager.CanPlay())
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
}
