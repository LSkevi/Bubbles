using UnityEngine;

public class Respawn : MonoBehaviour {
    public Transform spawnPoint;

    public void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player") {
            //pegar script do player, passar spawnPoint como spawnPoint
        }
    }
}