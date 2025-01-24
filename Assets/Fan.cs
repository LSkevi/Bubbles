using UnityEngine;

public class Fan : MonoBehaviour
{
    public Vector2 windDirection = Vector2.right; // Wind direction
    public float windForce = 5f; // Wind strength
    public bool isActive = true; // Fan state

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the fan is active and the object has the tag "Bubble"
        if (isActive && other.CompareTag("Bubble"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply force in the specified direction
                rb.AddForce(windDirection.normalized * windForce);
            }
        }
    }

    // Optional: Method to toggle the fan state
    public void ToggleFan()
    {
        isActive = !isActive;
    }

    // Visualization in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = isActive ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)windDirection.normalized * 2f);
    }
}
