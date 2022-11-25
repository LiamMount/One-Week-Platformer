using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeSpan;

    public GameObject fizzle;

    void Update()
    {
        if (lifeSpan > 0)
        {
            lifeSpan -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Fizzle()
    {
        GameObject newFizzle;
        newFizzle = Instantiate(fizzle, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Fizzle();
        }
        Destroy(this.gameObject);
    }
}
