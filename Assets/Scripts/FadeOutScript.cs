using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeOutScript : MonoBehaviour
{

    public int framesCounter;

    private void Start()
    {
        framesCounter = 0;
    }

    private void Update()
    {
        framesCounter++;


        if(framesCounter >= 250)
        {
            SceneManager.LoadScene("0_Title");
        }
    }
}
