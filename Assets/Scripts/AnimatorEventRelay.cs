using UnityEngine;

public class AnimatorEventRelay : MonoBehaviour
{
    public PlayerMovementTest playerMovementTest; // Refer�ncia ao script onde o ApplyJumpForce est�

    // Este m�todo ser� chamado pelo Animation Event
    public void TriggerJumpForce()
    {
        if (playerMovementTest != null)
        {
            playerMovementTest.ApplyJumpForce();
        }
        else
        {
            Debug.LogWarning("PlayerMovementTest n�o est� atribu�do no AnimatorEventRelay!");
        }
    }
}
