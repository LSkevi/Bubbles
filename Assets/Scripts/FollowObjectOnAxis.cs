using UnityEngine;

public class FollowObjectOnAxis : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target; // O GameObject que será seguido

    [Header("Follow Axes")]
    public bool followX = true; // Seguir no eixo X
    public bool followY = true; // Seguir no eixo Y
    public bool followZ = false; // Seguir no eixo Z

    [Header("Delay Settings")]
    public float horizontalFollowSpeed = 2f; // Velocidade para os lados (esquerda/direita)
    public float verticalFollowSpeedUp = 1f; // Velocidade para cima
    public float verticalFollowSpeedDown = 3f; // Velocidade para baixo

    private void Update()
    {
        if (target == null) return;

        // Obtém a posição atual do objeto e a posição do alvo
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(
            followX ? target.position.x : currentPosition.x,
            followY ? target.position.y : currentPosition.y,
            followZ ? target.position.z : currentPosition.z
        );

        // Calcula o delay para o eixo X
        float xLerpSpeed = followX ? horizontalFollowSpeed * Time.deltaTime : 1;

        // Calcula o delay para o eixo Y (separado para cima e para baixo)
        float yLerpSpeed = 1; // Por padrão, sem movimentação
        if (followY)
        {
            if (target.position.y > currentPosition.y)
            {
                // Movimento para cima
                yLerpSpeed = verticalFollowSpeedUp * Time.deltaTime;
            }
            else if (target.position.y < currentPosition.y)
            {
                // Movimento para baixo
                yLerpSpeed = verticalFollowSpeedDown * Time.deltaTime;
            }
        }

        // Interpola a posição com base nas velocidades específicas
        transform.position = new Vector3(
            Mathf.Lerp(currentPosition.x, targetPosition.x, xLerpSpeed),
            Mathf.Lerp(currentPosition.y, targetPosition.y, yLerpSpeed),
            followZ ? Mathf.Lerp(currentPosition.z, targetPosition.z, horizontalFollowSpeed * Time.deltaTime) : currentPosition.z
        );
    }
}
