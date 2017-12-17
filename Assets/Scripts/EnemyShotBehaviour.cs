using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotBehaviour : MonoBehaviour
{
    public PauseManager pause;
    public Transform enemyShot;
    public EnemyBehaviour enemy;
    public float speed = 6f;
    public bool facingRight;

    void Start()
    {
        enemyShot = GetComponent<Transform>();
        pause = GameObject.FindGameObjectWithTag("Manager").GetComponent<PauseManager>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
        if (enemy.isFacingRight) facingRight = true;
        else facingRight = false;

        Destroy(this.gameObject, 5f);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!pause.pause)
        {
            if (facingRight)
            {
                enemyShot.localPosition = new Vector3(enemyShot.localPosition.x + Time.deltaTime * speed, enemyShot.localPosition.y, enemyShot.localPosition.z);
            }
            else
            {
                enemyShot.localPosition = new Vector3(enemyShot.localPosition.x - Time.deltaTime * speed, enemyShot.localPosition.y, enemyShot.localPosition.z);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D Event " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log(collision);
            collision.GetComponent<CharacterBehaviour>().RecieveEnemyDamage(enemy.damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            Debug.Log("Destroyed by ground");
            Destroy(this.gameObject);
        }
    }
}
