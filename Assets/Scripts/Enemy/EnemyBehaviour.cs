using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum State { Active, Dead }
    public State state;

    public int life;
    public int damage;
    public float attackCD;
    public bool canAttack = false;
    public bool isAttacking = false;
    public bool isFacingRight = false;
    public AudioSource shotAudio;

    public PauseManager pause;
    public GameObject enemyShot;
    EnemyShotBehaviour enemyShotScript;
    public GameObject heart;
    public BoxCollider2D boxCollider2D;
    public Vector2 attackBoxPos;
    public Vector2 attackBoxSize;
    public Vector2 backBoxPos;
    public Vector2 backBoxSize;
    public ContactFilter2D filter;

    public SpriteRenderer rend;

    void Start ()
    {
        pause = GameObject.FindGameObjectWithTag("Manager").GetComponent<PauseManager>();
        enemyShotScript = enemyShot.GetComponent<EnemyShotBehaviour>();
        life = 10;
        damage = 1;
        attackCD = 2f;
        boxCollider2D = GetComponent<BoxCollider2D>();
        shotAudio = GetComponent<AudioSource>();
    }
	void Update ()
    {
        if (!pause.pause)
        {
            switch (state)
            {
                case State.Active:
                    Active();
                    break;
                case State.Dead:
                    Dead();
                    break;
                default:
                    break;
            }
        }
    }
    void Active()
    {
        Vector3 pos = new Vector2(this.transform.position.x, this.transform.position.y) + attackBoxPos;
        Collider2D[] results = new Collider2D[1];

        int numColliders = Physics2D.OverlapBox(pos, attackBoxSize, 0, filter, results);

        if(numColliders > 0)
        {
            canAttack = true;
        }
        else canAttack = false;

        Vector3 pos2 = new Vector2(this.transform.position.x, this.transform.position.y) + backBoxPos;
        Collider2D[] results2 = new Collider2D[1];

        int numColliders2 = Physics2D.OverlapBox(pos2, backBoxSize, 0, filter, results2);

        if(numColliders2 > 0)
        {
            Flip();
        }

        if (canAttack) Attacking();

        if (isAttacking)
        {
            attackCD -= Time.deltaTime;
            
            if (attackCD <= 0)
            {
                attackCD = 2f;
                isAttacking = false;

                    enemyShotScript.SetSpeed(isFacingRight);
                    Instantiate(enemyShot, new Vector3(this.transform.position.x, this.transform.position.y + attackBoxPos.y + 0.5f, 0), new Quaternion(0, 0, 0, 0));
            }
        }
    }
    void Attacking()
    {
        isAttacking = true;
    }
    void Dead()
    {
        Instantiate(heart, new Vector3(this.transform.position.x, this.transform.position.y + 1, 0), new Quaternion(0, 0, 0, 0));
        Destroy(this.gameObject);
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        rend.flipX = !rend.flipX;
        boxCollider2D.offset = new Vector2(boxCollider2D.offset.x * -1, boxCollider2D.offset.y);
        attackBoxPos.x *= -1;
        backBoxPos.x *= -1;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = new Vector2(this.transform.position.x, this.transform.position.y) + attackBoxPos;
        Gizmos.DrawWireCube(pos, attackBoxSize);
        Gizmos.color = Color.green;
        Vector3 pos2 = new Vector2(this.transform.position.x, this.transform.position.y) + backBoxPos;
        Gizmos.DrawWireCube(pos2, backBoxSize);
    }
    public void RecieveDamage(int damage)
    {
        shotAudio.Play();
        life -= damage;
        if (life <= 0) state = State.Dead;
    }
}
