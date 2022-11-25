using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    //Player controls
    public static bool isWalking, isJumping;

    //Level controls
    public static bool levelComplete, isPaused;

    //Respawn controls
    public static bool respawning;
    public static Transform respawnPoint;
    public static Rigidbody2D playerPrefab;
}
