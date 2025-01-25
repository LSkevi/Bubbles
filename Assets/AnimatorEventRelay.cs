using UnityEngine;

public class AnimatorEventRelay : MonoBehaviour
{
    public PlayerMovementTest playerMovementTest; // Referência ao script onde o ApplyJumpForce está

    // Este método será chamado pelo Animation Event
    public void TriggerJumpForce()
    {
        if (playerMovementTest != null)
        {
            playerMovementTest.ApplyJumpForce();
        }
        else
        {
            Debug.LogWarning("PlayerMovementTest não está atribuído no AnimatorEventRelay!");
        }
    }
}
