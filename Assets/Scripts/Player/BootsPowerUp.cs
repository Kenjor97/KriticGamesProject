using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsPowerUp : MonoBehaviour
{
    public CharacterBehaviour player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            player.hasBoots = true;
            this.gameObject.SetActive(false);
        }
    }
}
