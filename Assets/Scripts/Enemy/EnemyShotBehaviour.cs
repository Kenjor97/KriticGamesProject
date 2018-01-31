using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotBehaviour : MonoBehaviour
{
    public PauseManager pause;
    public EnemyBehaviour enemy;
    public float speed;
    public bool facingRight;

    void Start()
    {
        pause = GameObject.FindGameObjectWithTag("Manager").GetComponent<PauseManager>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();

        Destroy(this.gameObject, 5f);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!pause.pause)
        {
            this.transform.position = new Vector3(this.transform.position.x + Time.deltaTime * speed, this.transform.localPosition.y, this.transform.localPosition.z);
        }
    }

    public void SetSpeed(bool isFacingRight)
    {
        if(isFacingRight) speed = 6;
        else speed = -6;
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
