using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }
    public IEnumerator RespawnRoutine()
    {
        PlayerVariables.respawning = true;

        yield return new WaitForSeconds(1.25f);

        foreach (GameObject camProp in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(camProp);
        }

        // Reset shooters
        foreach (ShootingEnemy shootingScript in FindObjectsOfType<ShootingEnemy>())
        {
            shootingScript.shootTimer = 3f;
        }
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            bullet.GetComponent<Bullet>().Fizzle();
        }

        Rigidbody2D newPlayer;
        newPlayer = Instantiate(PlayerVariables.playerPrefab, PlayerVariables.respawnPoint.position, PlayerVariables.respawnPoint.rotation);
        PlayerVariables.respawning = false;
    }
}
