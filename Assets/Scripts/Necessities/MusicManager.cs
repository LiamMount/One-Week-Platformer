using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool stopPlaying;

    void Awake()
    {
        if (!stopPlaying)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            foreach (GameObject music in GameObject.FindGameObjectsWithTag("Music"))
            {
                Destroy(music);
            }
        }
    }
}
