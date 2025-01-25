using UnityEngine;

public class Fan : MonoBehaviour
{
    public Vector2 windDirection = Vector2.up;
    public float windSpeed = 5f;
    public bool isActive = true;

    private float originalYVelocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Bubble"))
        {
            Rigidbody2D bubbleRb = collision.GetComponent<Rigidbody2D>();
            originalYVelocity = bubbleRb.linearVelocity.y;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Bubble"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.linearVelocity = new Vector2(0, windSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            var xVelocity = collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity.x;
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(xVelocity, originalYVelocity);
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
