using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCoinBehaviour : MonoBehaviour
{
    //public int coinHeal;

	void Start ()
    {
        //coinHeal = 1;
	}

	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D Event " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log(collision);
            //collision.GetComponent<CharacterBehaviour>().RestoreLife(coinHeal);
            collision.GetComponent<CharacterBehaviour>().LifePowerUp();
            Destroy(this.gameObject);
        }
    }
}
