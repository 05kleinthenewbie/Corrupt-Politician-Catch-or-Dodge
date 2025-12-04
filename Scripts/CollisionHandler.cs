using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameManager gm;

    // Unity automatically calls this
    void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }

    // Public wrapper for tests
    public void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Money"))
        {
            gm.AddMoney(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Handcuff") || other.CompareTag("Bullet"))
        {
            gm.GameOver();
        }
    }
}
