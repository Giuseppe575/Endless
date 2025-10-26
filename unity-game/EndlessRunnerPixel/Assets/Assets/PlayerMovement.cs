using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;
    
    private int currentLane = 1;
    private Vector3 targetPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    void Update()
    {
        // Controlli per cambiare corsia
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0) currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 2) currentLane++;
        }
        
        // Movimento tra corsie (solo X)
        float targetX = (currentLane - 1) * laneDistance;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        
        Vector3 currentPos = transform.position;
        float newX = Mathf.Lerp(currentPos.x, targetPosition.x, laneChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, currentPos.y, currentPos.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISIONE con: " + collision.gameObject.name + " - Tag: " + collision.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit obstacle - calling GameOver");
            
            // Chiama il GameManager invece di fermare direttamente
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}