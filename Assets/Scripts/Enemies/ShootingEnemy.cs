using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    //private Animator animator; //Test

    public Transform shootPoint;

    public Rigidbody2D bullet;
    public float bulletSpeed;

    public float shootTimer = 3f;

    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer <= 0 && !PlayerVariables.levelComplete)
        {
            Fire();
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        shootTimer = 2f;

        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            //Left
            if (target.transform.position.x < this.transform.position.x)
            {
                if (transform.rotation.y != 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
                bulletClone.transform.Rotate(0, 0, 90);
                bulletClone.velocity = new Vector2(-1, 0) * bulletSpeed;
            }
            //Right
            else
            {
                if (transform.rotation.y != 180)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }

                Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
                bulletClone.transform.Rotate(0, 0, 90);
                bulletClone.velocity = new Vector2(1, 0) * bulletSpeed;
            }
        }
    }
}
