using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    public GameObject pauseMenu;
    public PauseManager pauseManager;
    public GameObject pauseGameObject;

	void Start ()
    {
        pauseManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PauseManager>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        pauseGameObject = GetComponent<GameObject>();
    }
	
	void Update ()
    {
        pauseMenu.SetActive(pauseManager.pause);
    }
}
