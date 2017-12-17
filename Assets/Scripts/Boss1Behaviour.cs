using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behaviour : MonoBehaviour
{
    public enum State { Active, Dead }
    public State state;

    public int life;
    public int damage;
    public float attackCD;
    public bool canAttack = false;
    public bool isAttacking = false;

    public PauseManager pause;
    public BoxCollider2D boxCollider2D;
    public Vector2 attackBoxPos;
    public Vector2 attackBoxSize;
    public ContactFilter2D filter;
    public SpriteRenderer rend;

    void Start ()
    {
        pause = GameObject.FindGameObjectWithTag("Manager").GetComponent<PauseManager>();
        life = 60;
        damage = 1;
        attackCD = 1f;
        boxCollider2D = GetComponent<BoxCollider2D>();
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

        if (numColliders > 0)
        {
            canAttack = true;
        }
        else canAttack = false;
    }
    void Attacking()
    {
        isAttacking = true;
    }
    void Dead()
    {
        Destroy(this.gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = new Vector2(this.transform.position.x, this.transform.position.y) + attackBoxPos;
        Gizmos.DrawWireCube(pos, attackBoxSize);
    }
    public void RecieveDamage(int damage)
    {
        life -= damage;
        if (life <= 0) state = State.Dead;
    }
}
