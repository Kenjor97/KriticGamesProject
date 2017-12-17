using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    public GameObject pauseMenu;
    public PauseManager pauseManager;

	void Start ()
    {
        pauseManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PauseManager>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
    }
	
	void Update ()
    {
        pauseMenu.SetActive(pauseManager.pause);
    }
}
