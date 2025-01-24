using UnityEngine;

public class Switch : MonoBehaviour
{
    public Fan fan; // Reference to the fan

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player activated the switch (or any other condition)
        if (other.CompareTag("Player"))
        {
            // Toggle the fan's state
            fan.ToggleFan();
        }
    }
}
