using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public CharacterBehaviour player;
    public Boss1Behaviour boss;
    public GameObject victory;
    public GameObject lose;
    public GameObject image;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss1Behaviour>();
        victory = GameObject.FindGameObjectWithTag("EndingWin");
        lose = GameObject.FindGameObjectWithTag("EndingLose");
        image = GameObject.FindGameObjectWithTag("EndingImage");
        image.SetActive(false);
        victory.SetActive(false);
        lose.SetActive(false);
    }
	
	void Update ()
    {
        if (player.state == CharacterBehaviour.State.Dead)
        {
            image.SetActive(true);
            victory.SetActive(false);
            lose.SetActive(true);
        }
        else if (boss.state == Boss1Behaviour.State.Dead)
        {
            image.SetActive(true);
            victory.SetActive(true);
            lose.SetActive(false);
        }
    }
}
