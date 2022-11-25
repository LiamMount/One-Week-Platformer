using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    public AudioSource source;
    public AudioClip checkSound;

    public GameObject stars;
    public Transform starPoint;

    bool activePoint = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SpawnParticles()
    {
        GameObject newStars;
        newStars = Instantiate(stars, starPoint.position, starPoint.rotation);
        source.PlayOneShot(checkSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerVariables.respawnPoint != this.transform && collision.gameObject.tag == "Player")
        {
            PlayerVariables.respawnPoint = this.transform;
            //Play animations
            if (!activePoint)
            {
                activePoint = true;
                anim.SetTrigger("activation");
            }
            else
            {
                SpawnParticles();
            }
        }
    }
}
