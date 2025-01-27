using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpFuncion : MonoBehaviour 
{ 
    public PlayerMovement pm;
    public event Action OnPlayerJump;
    int currentWalk = 0;
    
    public void Jump(InputAction.CallbackContext context) 
    {
        if(context.performed && pm.isGrounded)
        {
            pm.rb.linearVelocity = 
                new Vector2(pm.rb.linearVelocity.x, pm.jumpForce);
            //pm.OnPlayerJump?.Invoke();

            OnPlayerJump?.Invoke();
            AudioManager.Instance.PlaySFX(pm.jumpAudio);
        }

        if (context.canceled && pm.rb.linearVelocity.y > 0.1f)
        {
            pm.rb.linearVelocity = 
                new Vector2(pm.rb.linearVelocity.x, pm.jumpForce * 0.5f);

            OnPlayerJump?.Invoke();
            AudioManager.Instance.PlaySFX(pm.jumpAudio);
        }
    }

    public void WalkAudio() {
        AudioManager.Instance.PlaySFX(pm.walkAudio[currentWalk]);
        currentWalk++;
        if (currentWalk >= pm.walkAudio.Length) currentWalk = 0;
    }
}
