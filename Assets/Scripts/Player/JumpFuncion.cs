using System;
using UnityEngine;

public class JumpFuncion : MonoBehaviour 
{ 
    public PlayerMovement pm;
    public event Action OnPlayerJump;
    int currentWalk = 0;
    
    public void Jump() {
        pm.rb.linearVelocity = new Vector2(pm.rb.linearVelocity.x, pm.jumpForce);
        //pm.OnPlayerJump?.Invoke();
        OnPlayerJump?.Invoke();
        AudioManager.Instance.PlaySFX(pm.jumpAudio);
    }

    public void WalkAudio() {
        AudioManager.Instance.PlaySFX(pm.walkAudio[currentWalk]);
        currentWalk++;
        if (currentWalk >= pm.walkAudio.Length) currentWalk = 0;
    }
}
