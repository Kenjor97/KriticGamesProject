using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Transform projectile;
    public CharacterBehaviour player;

    void Start ()
    {
        projectile = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
    }
	
	void Update ()
    {

	}

    private void FixedUpdate()
    {
        projectile.localPosition = new Vector3(projectile.localPosition.x + 0.1f, projectile.localPosition.y, projectile.localPosition.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            
        }
    }
}
