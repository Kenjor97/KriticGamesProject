using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicFade : MonoBehaviour
{

    public AudioSource music;

    public float currentTime;
    public float timeDuration;
    public float delayTime;
	// Use this for initialization
	void Start ()
    {
        currentTime = 0;
        music = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            return;
        }

        if (currentTime <= timeDuration)
        {
            music.volume -= 0.001f;

        }
        currentTime += Time.deltaTime;



    }
}
