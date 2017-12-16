using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool pause = false;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Pause()
    {
        pause = !pause;
    }
}
