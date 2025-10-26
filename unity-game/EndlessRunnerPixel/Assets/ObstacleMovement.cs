using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float destroyZPosition = -10f;
    
    private bool isMoving = true;

    void Update()
    {
        if (!isMoving) return;
        
        // Muovi l'ostacolo verso il player (indietro lungo l'asse Z)
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        
        // Distruggi l'ostacolo quando esce dalla visuale
        if (transform.position.z < destroyZPosition)
        {
            Destroy(gameObject);
        }
    }

    public void StopMovement()
    {
        isMoving = false;
    }

    public void ResumeMovement()
    {
        isMoving = true;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}