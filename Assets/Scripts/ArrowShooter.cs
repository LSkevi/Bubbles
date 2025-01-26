using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // O prefab da flecha
    public Transform shootPoint;  // O local de onde as flechas serão disparadas
    public float shootInterval = 1f; // Intervalo entre os disparos
    public float arrowSpeed = 10f;   // Velocidade das flechas
    public float arrowLifetime = 3f; // Tempo de vida das flechas

    private float shootTimer; // Timer para controlar o intervalo de disparo

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            ShootArrow();
            shootTimer = 3f;
        }
    }

    void ShootArrow()
    {
        // Instancia a flecha no local do ponto de disparo
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);

        // Configura a velocidade da flecha
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = -shootPoint.right * arrowSpeed;
        }

        // Destroi a flecha após o tempo de vida configurado
        Destroy(arrow, arrowLifetime);
    }
}
