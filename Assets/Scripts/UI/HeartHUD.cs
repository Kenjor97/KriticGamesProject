using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHUD : MonoBehaviour
{
    public Sprite[] heartSprites;
    public Image heartUI;
    public CharacterBehaviour player;

	void Start ()
    {
        heartUI = GameObject.FindGameObjectWithTag("Hearts").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
    }
	
	void Update ()
    {
        heartUI.sprite = heartSprites[player.life];
	}
}
