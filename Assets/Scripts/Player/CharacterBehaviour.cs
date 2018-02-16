using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collisions))]
public class CharacterBehaviour : MonoBehaviour
{
    public enum State { Default, Dead, GodMode }
    public State state;
    public CameraBehaviour cameraBehaviour;
    public PauseManager pause;
    public GameObject projectile;
    public Transform playerTransform;
    public Animator anim;
    public int maxLife;
    public int life;
    public int lifePowerUp;
    public int meleeDamage;
    public int rangedDamage;
    [Header("State")]
    public bool canMove = true;
    public bool isMoving = false;
    public bool canJump = true;
    public bool isFacingRight = true;
    public bool isJumping = false;
    public bool isRunning = false;
    public bool crouch = false;
    public bool canDash = true;
    public bool canRecieveDamage = true;
    public bool canDoubleJump = false;
    public bool onLadder = false;
    public bool canWallJump = false;
    public bool isWallJumping = false;
    public bool isDashing = false;
    [Header("Power Ups")]
    public bool hasBoots = false;
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
    public Vector2 attackBoxPos;
    public Vector2 attackBoxSize;
    public ContactFilter2D filter;

    void Start ()
    {
        collisions = GetComponent<Collisions>();
        pause = GameObject.FindGameObjectWithTag("Manager").GetComponent<PauseManager>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerTransform = GetComponent<Transform>();
        maxLife = 3;
        life = maxLife;
        lifePowerUp = 0;
        meleeDamage = 5;
        rangedDamage = 2;
        dashCD = 0.2f;
        standYSize = boxCollider2D.size.y;
        crouchYSize = boxCollider2D.size.y / 2;
        standYOffset = boxCollider2D.offset.y;
        crouchYOffset = boxCollider2D.offset.y / 2 + 0.065f;
	}
	
	void Update ()
    {
        if (!pause.pause)
        {
            Time.timeScale = 1;
            switch (state)
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
        else Time.timeScale = 0;
    }

    private void FixedUpdate()
    {
        if (!pause.pause)
        {
            if (state == State.Default)
            {
                collisions.MyFixedUpdate();
                Physics2D.IgnoreLayerCollision(8, 9, false);
                canRecieveDamage = true;
            }

            if (axis.x != 0) isMoving = true;
            else if (axis.x == 0) isMoving = false;
            if (isMoving && collisions.isGrounded) anim.SetBool("isMoving", true);
            else
            {
                axis.x = 0;
                anim.SetBool("isMoving", false);
            }

            if (isJumping)
            {
                isJumping = false;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (isWallJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //rb.AddForce(new Vector2(-wallJumpForce, jumpForce), ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                if (isFacingRight)
                {
                    rb.AddForce(Vector2.left * wallJumpForce, ForceMode2D.Impulse);
                    //rb.AddForce(new Vector2(-1 * wallJumpForce, 1 * wallJumpForce), ForceMode2D.Impulse);
                }
                else if (!isFacingRight)
                {
                    rb.AddForce(Vector2.right * wallJumpForce, ForceMode2D.Impulse);
                    //rb.AddForce(new Vector2(1 * wallJumpForce, 1 * wallJumpForce), ForceMode2D.Impulse);
                }
                isWallJumping = false;
                canWallJump = true;
            }

            rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);

            if (onLadder)
            {
                canDoubleJump = false;
                rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
            }

            if (crouch)
            {
                canMove = false;
                //boxCollider2D.size = new Vector2(boxCollider2D.size.x, crouchYSize);
                //boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, crouchYOffset);
                playerTransform.localScale = new Vector3(playerTransform.localScale.x, 0.6f, playerTransform.localScale.z);
            }
            else
            {
                canMove = true;
                crouch = false;
                //boxCollider2D.size = new Vector2(boxCollider2D.size.x, standYSize);
                //boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, standYOffset);
                playerTransform.localScale = new Vector3(playerTransform.localScale.x, 1f, playerTransform.localScale.z);
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
    }

    protected virtual void DefaultUpdate()
    {
        // Calcular el movimiento horizontal
        HorizontalMovement();
        // Calcular el movimiento vertical
        VerticalMovement();
    }

    #region MOVEMENT
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
    #endregion

    #region MECHANICS
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
    
    void Crouching()
    {
        crouch = true;
    }
    #endregion

    void Dead()
    {
        canMove = false;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Gizmos.DrawWireCube(pos, attackBoxSize);
    }

    #region GOD MODE
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
    #endregion

    #region COLLIDER TRIGGERS
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
    #endregion

    #region PUBLIC METHODS
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    public void JumpStart() //Decidir como será el salto
    {
        if(!canJump) return;

        if(collisions.isGrounded)
        {
            if(isRunning) jumpForce = jumpRunForce;
            else jumpForce = jumpWalkForce;
            Jump();
        }

        if(collisions.isFalling && canDoubleJump && hasBoots)
        {
            if(isRunning) jumpForce = jumpRunForce;
            else jumpForce = jumpWalkForce;
            DoubleJump();
        }

        if(collisions.isFalling && canWallJump && hasBoots)
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
        if(canDash && hasBoots)
        {
            Dashing();
        }
    }
    public void Attack()
    {
        anim.SetTrigger("attack00");
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Collider2D[] results = new Collider2D[1];

        int numColliders = Physics2D.OverlapBox(pos, attackBoxSize, 0, filter, results);

        if (numColliders > 0)
        {
            Debug.Log("Attacking Enemy");
            if(results[0].tag == "Enemy") results[0].GetComponent<EnemyBehaviour>().RecieveDamage(meleeDamage);
            if(results[0].tag == "Boss") results[0].GetComponent<Boss1Behaviour>().RecieveDamage(meleeDamage);
        }
    }
    public void Attack2()
    {
        Instantiate(projectile, new Vector3(this.transform.position.x + attackBoxPos.x, this.transform.position.y + attackBoxPos.y, 0), new Quaternion(0, 0, 0, 0));
    }
    public void RestoreLife(int heal)
    {
        life += heal;
        if (life >= maxLife) life = maxLife;
    }
    public void LifePowerUp()
    {
        lifePowerUp++;
        if(lifePowerUp == 3)
        {
            lifePowerUp = 0;
            maxLife++;
            life = maxLife;
        }
    }
    public void RecieveEnemyDamage(int damage)
    {
        if (canRecieveDamage)
        {
            life -= damage;
            if (life <= 0)
            {
                life = 0;
                state = State.Dead;
            }
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