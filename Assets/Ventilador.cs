using UnityEngine;

public class Ventilador : MonoBehaviour
{
    public Vector2 forceDirection = Vector2.right; // Direção do vento
    public float forceStrength = 5f; // Intensidade da força
    public bool isOn = true; // Estado do ventilador

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verifica se o ventilador está ligado e se o objeto tem a tag "Bolha"
        if (isOn && other.CompareTag("Bolha"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Aplica força na direção definida
                rb.AddForce(forceDirection.normalized * forceStrength);
            }
        }
    }

    // Opcional: Método para alternar o estado do ventilador
    public void ToggleVentilador()
    {
        isOn = !isOn;
    }

    // Visualização no editor
    private void OnDrawGizmos()
    {
        Gizmos.color = isOn ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)forceDirection.normalized * 2f);
    }
}
