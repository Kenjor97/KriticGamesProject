using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack, Dead }
    public State state;

    public int life;

    public SpriteRenderer rend;

    void Start ()
    {
        life = 10;
	}
	void Update ()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                break;
            case State.Attack:
                break;
            case State.Dead:
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

    }
    public void RecieveDamage(int damage)
    {
        life -= damage;
        if (life <= 0) state = State.Dead;
    }
}
