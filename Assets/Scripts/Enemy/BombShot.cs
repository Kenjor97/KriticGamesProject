using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShot : MonoBehaviour
{
    public PauseManager pause;
    public Transform bombShot;
    public Boss1Behaviour boss;
    public float speed = 10f;
    public Transform player;
    public Vector2 playerPos;
    public Vector2 direction;

    void Start()
    {
        bombShot = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pause = GameObject.FindGameObjectWithTag("Manager").GetComponent<PauseManager>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss1Behaviour>();
        playerPos = player.transform.position;
        direction = (new Vector2(playerPos.x, playerPos.y + 1) - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;

        Destroy(this.gameObject, 5f);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!pause.pause)
        {
            this.transform.Translate(new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D Event " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log(collision);
            collision.GetComponent<CharacterBehaviour>().RecieveEnemyDamage(boss.damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            Debug.Log("Destroyed by ground");
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("projectile"))
        {
            Debug.Log("Destroyed by" + collision);
            Destroy(this.gameObject);
        }
    }
}
