using UnityEngine;

public class Fan : MonoBehaviour
{
    public Vector2 windDirection = Vector2.right; // Dire��o do vento
    public float windForce = 5f; // Intensidade do vento
    public bool isActive = true; // Estado do ventilador

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verifica se o ventilador est� ativo e o objeto tem a tag "Bubble"
        if (isActive && other.CompareTag("Bubble"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Aplica for�a na dire��o do vento
                rb.AddForce(windDirection.normalized * windForce);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Quando o objeto sai do vento
        if (other.CompareTag("Bubble"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Redefine a velocidade para subir reto (apenas no eixo Y)
                rb.linearVelocity = new Vector2(0, Mathf.Abs(rb.linearVelocity.y));
            }
        }
    }

    // M�todo para alternar o estado do ventilador
    public void ToggleFan()
    {
        isActive = !isActive;
    }

    // Visualiza��o no editor
    private void OnDrawGizmos()
    {
        Gizmos.color = isActive ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)windDirection.normalized * 2f);
    }
}
