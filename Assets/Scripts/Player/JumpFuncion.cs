using System;
using UnityEngine;

public class JumpFuncion : MonoBehaviour 
{ 
    public PlayerMovement pm;
    public event Action OnPlayerJump;
    
    public void Jump() {
        pm.rb.linearVelocity = new Vector2(pm.rb.linearVelocity.x, pm.jumpForce);
        //pm.OnPlayerJump?.Invoke();
        OnPlayerJump?.Invoke();
        AudioManager.Instance.PlaySFX(pm.jumpAudio);
    }
}
