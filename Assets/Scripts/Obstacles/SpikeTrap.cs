using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 1;
    public Transform ycomparePoint;
    public float impulseForce = 7f;
    private float impulseDirection;

    private void CalculateDirection(float playerY)
    {
        float spikeY = ycomparePoint.position.y;

        if (spikeY <= playerY) impulseDirection = 1;
        else impulseDirection = -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerRb = collision.GetComponent<Rigidbody2D>();
            var playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerRb != null)
            {
                CalculateDirection(collision.transform.position.y);

                playerRb.linearVelocity =
                    new Vector2(playerRb.linearVelocity.x, impulseForce * impulseDirection);

                Debug.Log("Bounce!");
            }

            if (playerHealth != null)
                playerHealth.TakeDamage(damage);
        }
    }
}
