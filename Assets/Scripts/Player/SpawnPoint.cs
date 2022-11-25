using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Rigidbody2D playerPrefab;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerVariables.playerPrefab = playerPrefab;
        PlayerVariables.respawnPoint = this.transform;

        Rigidbody2D newPlayer;
        newPlayer = Instantiate(playerPrefab, this.transform.position, this.transform.rotation);
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(transform.position, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
