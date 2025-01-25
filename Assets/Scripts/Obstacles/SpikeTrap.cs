using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 1;
    public Transform ycomparePoint;
    public float yImpulseForce = 7f;
    private float impulseDirection;

    public float xMovementDeceleration = 0.5f;
    public float decelerationDuration = 1f;

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
                    new Vector2(playerRb.linearVelocity.x, yImpulseForce * impulseDirection);
            }

            if (playerHealth != null)
                playerHealth.TakeDamage(damage);
        }
    }
}
