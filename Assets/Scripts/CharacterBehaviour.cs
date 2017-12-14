using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collisions))]
public class CharacterBehaviour : MonoBehaviour
{
    public enum State { Default, Dead, GodMode }
    public State state;
    public CameraBehaviour cameraBehaviour;
    public GameObject projectile;
    public int life;
    public int meleeDamage;
    public int rangedDamage;
    [Header("State")]
    public bool canMove = true;
    public bool canJump = true;
    public bool isFacingRight = true;
    public bool isJumping = false;
    public bool isRunning = false;
    public bool crouch = false;
    public bool canDash = true;
    public bool canRecieveDamage = true;
    //public bool isLookingUp = false;
    //public bool isLookingDown = false;
    public bool canDoubleJump = false;
    public bool onLadder = false;
    public bool canWallJump = false;
    public bool isWallJumping = false;
    public bool isDashing = false;
    [Header("Physics")]
    public Rigidbody2D rb;
    public Collisions collisions;
    public BoxCollider2D boxCollider2D;
    [Header("Speed")]
    public float walkSpeed;
    public float runSpeed;
    public float movementSpeed;
    public float horizontalSpeed;
    public float onLadderSpeed;
    public float verticalSpeed;
    public Vector2 axis;
    [Header("Forces")]
    public float jumpWalkForce;
    public float jumpRunForce;
    public float jumpForce;
    public float wallJumpForce;
    public float dashForce;
    [Header("Graphics")]
    public SpriteRenderer rend;
    [Header("Collider Values")]
    public float standYSize;
    public float crouchYSize;
    public float standYOffset;
    public float crouchYOffset;
    [Header("Other")]
    [SerializeField] float dashCD;
    //[SerializeField] float wallJumpCD;
    public Vector2 attackBoxPos;
    public Vector2 attackBoxSize;
    public ContactFilter2D filter;

    void Start ()
    {
        collisions = GetComponent<Collisions>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        life = 10;
        meleeDamage = 5;
        rangedDamage = 2;
        dashCD = 0.2f;
        //wallJumpCD = 0.2f;
        standYSize = boxCollider2D.size.y;
        crouchYSize = boxCollider2D.size.y / 2;
        standYOffset = boxCollider2D.offset.y;
        crouchYOffset = boxCollider2D.offset.y / 2 + 0.065f;
	}
	
	void Update ()
    {
        switch(state)
        {
            case State.Default:
                DefaultUpdate();
                break;
            case State.Dead:
                Dead();
                HorizontalMovement();
                break;
            case State.GodMode:
                DefaultUpdate();
                Invulnerable();
                Fly();
                Ghost();
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if(state == State.Default)
        {
            collisions.MyFixedUpdate();
            Physics2D.IgnoreLayerCollision(8, 9, false);
        }

        if(isJumping)
        {
            isJumping = false;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (isWallJumping)
        {
            //wallJumpCD -= Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            //rb.AddForce(new Vector2(-wallJumpForce, jumpForce), ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if(isFacingRight)
            {
                rb.AddForce(Vector2.left * wallJumpForce, ForceMode2D.Impulse);
                //rb.AddForce(new Vector2(-1 * wallJumpForce, 1 * wallJumpForce), ForceMode2D.Impulse);
            }
            else if(!isFacingRight)
            {
                rb.AddForce(Vector2.right * wallJumpForce, ForceMode2D.Impulse);
                //rb.AddForce(new Vector2(1 * wallJumpForce, 1 * wallJumpForce), ForceMode2D.Impulse);
            }
            //if(wallJumpCD <=0)
            //{
            //    wallJumpCD = 0.2f;
            //    isWallJumping = false;
            //    canWallJump = true;
            //}
            isWallJumping = false;
            canWallJump = true;
        }
        
        rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);

        if(onLadder)
        {
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
        }

        if (crouch)
        {
            canMove = false;
            boxCollider2D.size = new Vector2(boxCollider2D.size.x, crouchYSize);
            boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, crouchYOffset);
        }
        else
        {
            canMove = true;
            crouch = false;
            boxCollider2D.size = new Vector2(boxCollider2D.size.x, standYSize);
            boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, standYOffset);
        }

        if (isDashing)
        {
            dashCD -= Time.deltaTime;
            if (isFacingRight)
            {
                rb.velocity = new Vector2(0, 0);

                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            }
            else if (!isFacingRight)
            {
                rb.velocity = new Vector2(0, 0);

                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
            }
            if (dashCD <= 0)
            {
                dashCD = 0.2f;
                isDashing = false;
                canDash = true;
            }
        }
    }

    protected virtual void DefaultUpdate()
    {
        // Calcular el movimiento horizontal
        HorizontalMovement();
        // Calcular el movimiento vertical
        VerticalMovement();
    }

    void Dead()
    {
        canMove = false;
    }

    void HorizontalMovement()
    {
        if(!canMove)
        {
            horizontalSpeed = 0;
            return;
        }
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //Punto muerto
        if(-0.1f < axis.x && axis.x < 0.1f)
        {
            if(collisions.isGrounded)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            horizontalSpeed = 0;
            return;
        }
        //Si toca la pared
        if(collisions.isWalled && collisions.isFalling)
        {
            canWallJump = true;
            //canDoubleJump = false;
        }
        else canWallJump = false;

        if (collisions.isWalled)
        {
            if ((isFacingRight && axis.x > 0) || (!isFacingRight && axis.x < 0))
            {
                horizontalSpeed = 0;
                return;
            }
        }

        if(isFacingRight && axis.x < 0) Flip();
        if(!isFacingRight && axis.x > 0) Flip();

        if(isRunning) movementSpeed = runSpeed;
        else movementSpeed = walkSpeed;

        horizontalSpeed = axis.x * movementSpeed;
    }
    void VerticalMovement()
    {
        crouch = false;
        //isLookingDown = false;
        //isLookingUp = false;
        verticalSpeed = axis.y * onLadderSpeed;        
    }
    void Jump()
    {
        isJumping = true;
    }
    void DoubleJump()
    {
        isJumping = true;
        canDoubleJump = false;
    }
    void WallJump()
    {
        isWallJumping = true;
        canWallJump = false;
    }
    void Dashing()
    {
        isDashing = true;
        canDash = false;
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        rend.flipX = !rend.flipX;
        boxCollider2D.offset = new Vector2(boxCollider2D.offset.x * -1, boxCollider2D.offset.y);
        collisions.Flip(isFacingRight);
        cameraBehaviour.offSet.x *= -1;
        attackBoxPos.x *= -1;
    }
    void Crouching()
    {
        crouch = true;
    }
    void Invulnerable()
    {
        canRecieveDamage = false;
    }
    void Fly()
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
    }
    void Ghost()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Gizmos.DrawWireCube(pos, attackBoxSize);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("ladder"))
        {
            onLadder = true;
            rb.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("ladder"))
        {
            onLadder = false;
            rb.gravityScale = 1;
        }
    }
    #region Public
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    public void JumpStart() //Decidir como será el salto
    {
        if(!canJump) return;

        if(collisions.isGrounded)
        {
            /*if(isLookingDown)
            {
                Debug.Log("bajar plataforma");
            }*/

            if(isRunning) jumpForce = jumpRunForce;
            else jumpForce = jumpWalkForce;
            Jump();
        }

        if(collisions.isFalling && canDoubleJump)
        {
            if(isRunning) jumpForce = jumpRunForce;
            else jumpForce = jumpWalkForce;
            DoubleJump();
        }

        if(collisions.isFalling && canWallJump)
        {
            //if (isRunning) jumpForce = jumpRunForce;
            //else jumpForce = jumpWalkForce;
            WallJump();
        }
    }
    public void Crouch()
    {
        if (collisions.isGrounded)
        {
            Crouching();
        }
    }
    public void Dash()
    {
        if(canDash)
        {
            Dashing();
        }
    }
    public void Attack()
    {
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Collider2D[] results = new Collider2D[1];

        int numColliders = Physics2D.OverlapBox(pos, attackBoxSize, 0, filter, results);

        if (numColliders > 0)
        {
            Debug.Log("Attacking Enemy");
            results[0].GetComponent<EnemyBehaviour>().RecieveDamage(meleeDamage);
        }
    }
    public void Attack2()
    {
        //Instantiate(projectile, this.transform.position);
    }
    public void RecieveEnemyDamage(int damage)
    {
        if (canRecieveDamage)
        {
            life -= damage;
            if (life <= 0) state = State.Dead;
        }
    }
    public void RecieveHazardDamage()
    {
        if (canRecieveDamage)
        {
            life = 0;
            if (life <= 0) state = State.Dead;
        }
    }
    public void GodMode()
    {
        if (state == State.Default) state = State.GodMode;
        else if (state == State.GodMode)
        {
            rb.gravityScale = 1;
            state = State.Default;
        }
    }
    #endregion
}