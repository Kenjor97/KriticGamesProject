using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screens : MonoBehaviour
{
    public CharacterBehaviour player;
    public Boss1Behaviour boss;
    
	void Start ()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
		boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss1Behaviour>();
    }
	
	void Update ()
    {
        if(player.state == CharacterBehaviour.State.Dead) SceneManager.LoadScene("3_EndingLose");
        else if(boss.state == Boss1Behaviour.State.Dead) SceneManager.LoadScene("2_EndingWin");
    }
    
}
