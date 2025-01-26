using UnityEngine;

public class JumpFuncion : MonoBehaviour {
    public PlayerMovement pm;
    public void Jump() {
        pm.rb.linearVelocity = new Vector2(pm.rb.linearVelocity.x, pm.jumpForce);
        //pm.OnPlayerJump?.Invoke();

        AudioManager.Instance.PlaySFX(pm.jumpAudio);
    }
}
