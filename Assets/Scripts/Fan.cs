using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public Vector2 windDirection = Vector2.up;
    public float yWindSpeed = 1.5f;
    public bool isActive = true;

    private List<Rigidbody2D> bubblesRbsList = new List<Rigidbody2D>();
    private Dictionary<Rigidbody2D, float> originalGravityScaleDict = new Dictionary<Rigidbody2D, float>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Bubble"))
        {
            var rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null && !bubblesRbsList.Contains(rb))
            {
                // Tag de isOnWind
                collision.gameObject.GetComponent<BubbleBase>().isOnWind = true;
                bubblesRbsList.Add(rb);
            }

            if (!originalGravityScaleDict.ContainsKey(rb))
            {
                originalGravityScaleDict[rb] = rb.gravityScale;
                rb.gravityScale = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Aplica o efeito do vento a todas as bolhas na lista
        foreach (var bubbleRb in bubblesRbsList)
        {
            if (bubbleRb != null)
            {
                var windEffect = yWindSpeed * Time.deltaTime;
                bubbleRb.linearVelocity = new Vector2(0, bubbleRb.linearVelocity.y + windEffect);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            var rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null && bubblesRbsList.Contains(rb))
            {
                // Tag de isOnWind
                collision.gameObject.GetComponent<BubbleBase>().isOnWind = false;

                // Restaura a gravidade original da bolha que está saindo
                if (originalGravityScaleDict.ContainsKey(rb))
                {
                    rb.gravityScale = originalGravityScaleDict[rb];
                    originalGravityScaleDict.Remove(rb);
                }

                // Remove o Rigidbody2D da lista
                bubblesRbsList.Remove(rb);
            }
        }
    }

    public void ToggleFan() => isActive = !isActive;

    private void OnDrawGizmos()
    {
        Gizmos.color = isActive ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)windDirection.normalized * 2f);
    }
}
