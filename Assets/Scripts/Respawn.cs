using UnityEngine;

public class Respawn : MonoBehaviour {
    public Transform spawnPoint;

    public void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player") {
            col.GetComponent<PlayerHealth>().spawnPoint = spawnPoint.position;
            col.GetComponent<PlayerHealth>().ReloadAmmo();
        }
    }
}