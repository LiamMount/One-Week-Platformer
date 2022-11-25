using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public Animator anim;
    public AudioSource source;
    public AudioClip successSound;

    public GameObject stars;
    public Transform starPoint;

    public string sceneToLoad;

    void Start()
    {
        PlayerVariables.levelComplete = false;
    }

    public void SpawnParticles()
    {
        GameObject newStars;
        newStars = Instantiate(stars, starPoint.position, starPoint.rotation);
        source.PlayOneShot(successSound);
    }

    public IEnumerator BeatLevel()
    {
        // Reset shooters
        foreach (ShootingEnemy shootingScript in FindObjectsOfType<ShootingEnemy>())
        {
            shootingScript.shootTimer = 100f;
        }
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            bullet.GetComponent<Bullet>().Fizzle();
        }

        yield return new WaitForSeconds(1f);

        SpawnParticles();

        yield return new WaitForSeconds(4f);

        //Load new scene
        if (sceneToLoad == null)
        {
            Debug.Log("Nothing to load");
        }
        else
        {
            //Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerVariables.levelComplete = true;

            anim.SetTrigger("activation");
            collision.gameObject.SetActive(false);

            StartCoroutine(BeatLevel());
        }
    }
}
