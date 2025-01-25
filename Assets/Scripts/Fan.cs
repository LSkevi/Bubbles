using UnityEngine;
using System.Collections.Generic;

public class Fan : MonoBehaviour
{
    public Vector2 windDirection = Vector2.up;
    public float windSpeed = 5f;
    public bool isActive = true;

    private Dictionary<Rigidbody2D, float> originalSpeeds = new Dictionary<Rigidbody2D, float>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Bubble"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (!originalSpeeds.ContainsKey(rb))
                {
                    originalSpeeds[rb] = rb.linearVelocity.y;
                }
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y + windSpeed);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Bubble"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(0, windSpeed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && originalSpeeds.ContainsKey(rb))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, originalSpeeds[rb]);
                originalSpeeds.Remove(rb);
            }
        }
    }

    public void ToggleFan()
    {
        isActive = !isActive;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isActive ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)windDirection.normalized * 2f);
    }
}
