using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack, Dead }
    public State state;

    public int life;
    public float attackCD;
    public bool canAttack;

    public SpriteRenderer rend;

    void Start ()
    {
        life = 10;
        attackCD = 0.5f;
        canAttack = false;
	}
	void Update ()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Dead:
                Dead();
                break;
            default:
                break;
        }
    }
    void Patrol()
    {

    }
    void Chase()
    {

    }
    void Attack()
    {

    }
    void Dead()
    {
        Destroy(this.gameObject);
    }
    public void RecieveDamage(int damage)
    {
        life -= damage;
        if (life <= 0) state = State.Dead;
    }
}
