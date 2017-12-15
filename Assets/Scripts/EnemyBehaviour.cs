using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum State { Detection, Attack, Dead }
    public State state;

    public int life;
    public float attackCD;
    public bool canAttack = false;
    public bool isFacingRight = false;

    public BoxCollider2D boxCollider2D;
    public Vector2 attackBoxPos;
    public Vector2 attackBoxSize;
    public Vector2 backBoxPos;
    public Vector2 backBoxSize;
    public ContactFilter2D filter;

    public SpriteRenderer rend;

    void Start ()
    {
        life = 10;
        attackCD = 1.5f;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
	void Update ()
    {
        switch (state)
        {
            case State.Detection:
                Detection();
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
    void Detection()
    {
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Collider2D[] results = new Collider2D[1];

        int numColliders = Physics2D.OverlapBox(pos, attackBoxPos, 0, filter, results);

        if(numColliders > 0)
        {
            canAttack = true;
        }
        else canAttack = false;

        Vector3 pos2 = this.transform.position + (Vector3)backBoxPos;
        Collider2D[] results2 = new Collider2D[1];

        int numColliders2 = Physics2D.OverlapBox(pos2, backBoxPos, 0, filter, results2);

        if(numColliders2 > 0)
        {
            Flip();
        }
    }
    void Attack()
    {

    }
    void Dead()
    {
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
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Gizmos.DrawWireCube(pos, attackBoxSize);
        Gizmos.color = Color.green;
        Vector3 pos2 = this.transform.position + (Vector3)backBoxPos;
        Gizmos.DrawWireCube(pos2, backBoxSize);
    }
    public void RecieveDamage(int damage)
    {
        life -= damage;
        if (life <= 0) state = State.Dead;
    }
}
