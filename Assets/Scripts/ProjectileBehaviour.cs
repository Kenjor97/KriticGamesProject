using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Transform projectile;
    public CharacterBehaviour player;
    public float speed = 7.5f;
    public float rotateSpeed = 500;
    public bool facingRight;

    void Start ()
    {
        projectile = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
        if (player.isFacingRight) facingRight = true;
        else facingRight = false;

        Destroy(this.gameObject, 2.5f);
    }
	
	void Update ()
    {

	}

    private void FixedUpdate()
    {
        if (facingRight)
        {
            projectile.localPosition = new Vector3(projectile.localPosition.x + Time.deltaTime * speed, projectile.localPosition.y, projectile.localPosition.z);
            projectile.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        }
        else
        {
            projectile.localPosition = new Vector3(projectile.localPosition.x - Time.deltaTime * speed, projectile.localPosition.y, projectile.localPosition.z);
            projectile.Rotate(Vector3.forward * Time.deltaTime * -rotateSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D Event " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            Debug.Log("Enemy: " + collision);
            collision.GetComponent<EnemyBehaviour>().RecieveDamage(player.rangedDamage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            Debug.Log("Destroyed by ground");
            Destroy(this.gameObject);
        }
    }
}
