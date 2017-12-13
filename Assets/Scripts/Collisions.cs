using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public CharacterBehaviour player;

    [Header("Ground State")]
    public bool isGrounded;
    public bool wasGroundedLastFrame;
    public bool justGotGrounded;
    public bool justNotGrounded;
    public bool isFalling;

    [Header("Wall State")]
    public bool isWalled;
    public bool wasWalledLastFrame;
    public bool justGotWalled;
    public bool justNotWalled;
    public bool isNotWalling;

    [Header("Ceiling State")]
    public bool isCeiled;
    public bool wasCeiledLastFrame;
    public bool justGotCeiled;
    public bool justNotCeiled;
    public bool isNotCeiling;

    [Header("Filter")]
    public ContactFilter2D filter;
    public int maxColliders = 1;
    public bool checkGround = true;
    public bool checkWall = true;
    public bool checkCeiling = true;

    [Header("Box properties")]
    public Vector2 groundBoxPos;
    public Vector2 groundBoxSize;

    public Vector2 wallBoxPos;
    public Vector2 wallBoxSize;

    public Vector2 ceilingBoxPos;
    public Vector2 ceilingBoxSize;

    public void MyFixedUpdate()
    {
        if (player.state == CharacterBehaviour.State.Default)
        {
            ResetState();
            GroundDetection();
            WallDetection();
            CeilingDetection();
            if (justNotWalled && isGrounded) player.canDoubleJump = false;
            if (!isGrounded && isWalled && !player.isWallJumping) player.canWallJump = true;
            else player.canWallJump = false;
        }
    }

    void ResetState()
    {
        wasGroundedLastFrame = isGrounded;
        isFalling = !isGrounded;

        isGrounded = false;
        justNotGrounded = false;
        justGotGrounded = false;

        wasWalledLastFrame = isWalled;
        isNotWalling = !isWalled;

        isWalled = false;
        justNotWalled = false;
        justGotWalled = false;

        wasCeiledLastFrame = isCeiled;
        isNotCeiling = !isCeiled;

        isCeiled = false;
        justNotCeiled = false;
        justGotCeiled = false;
    }
    void GroundDetection()
    {
        if(!checkGround) return;

        Vector3 pos = this.transform.position + (Vector3)groundBoxPos;
        Collider2D[] results = new Collider2D[maxColliders];

        int numColliders = Physics2D.OverlapBox(pos, groundBoxSize, 0, filter, results);

        if(numColliders > 0)
        {
            isGrounded = true;
            //player.canDoubleJump = false;
        }
        if (!wasGroundedLastFrame && isGrounded)
        {
            justGotGrounded = true;
            player.canDoubleJump = false;
        }
        if(wasGroundedLastFrame && !isGrounded)
        {
            justNotGrounded = true;
            player.canDoubleJump = true;
        }

        if(justNotGrounded) Debug.Log("JUST NOT GROUNDED");
        if(justGotGrounded) Debug.Log("just got grounded");
    }
    void WallDetection()
    {
        if (!checkWall) return;

        Vector3 pos = this.transform.position + (Vector3)wallBoxPos;
        Collider2D[] results = new Collider2D[maxColliders];

        int numColliders = Physics2D.OverlapBox(pos, wallBoxSize, 0, filter, results);

        if (numColliders > 0)
        {
            isWalled = true;
            player.canDoubleJump = false;
        }
        if (!wasWalledLastFrame && isWalled)
        {
            justGotWalled = true;
            //player.canDoubleJump = false;
        }
        if (wasWalledLastFrame && !isWalled)
        {
            justNotWalled = true;
            player.canDoubleJump = true;
        }

        if (justNotWalled) Debug.Log("JUST NOT WALLED");
        if (justGotWalled) Debug.Log("just got walled");
    }
    void CeilingDetection()
    {
        if (!checkCeiling) return;

        Vector3 pos = this.transform.position + (Vector3)ceilingBoxPos;
        Collider2D[] results = new Collider2D[maxColliders];

        int numColliders = Physics2D.OverlapBox(pos, ceilingBoxSize, 0, filter, results);

        if (numColliders > 0)
        {
            isCeiled = true;
        }
        if (!wasCeiledLastFrame && isCeiled) justGotCeiled = true;
        if (wasCeiledLastFrame && !isCeiled) justNotCeiled = true;

        if (justNotCeiled) Debug.Log("JUST NOT CEILED");
        if (justGotCeiled) Debug.Log("just got ceiled");
    }
    
    public void Flip(bool isFacingRight)
    {
        wallBoxPos.x = -wallBoxPos.x;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = this.transform.position + (Vector3)groundBoxPos;
        Gizmos.DrawWireCube(pos, groundBoxSize);

        Gizmos.color = Color.red;
        Vector3 pos2 = this.transform.position + (Vector3)wallBoxPos;
        Gizmos.DrawWireCube(pos2, wallBoxSize);

        Gizmos.color = Color.red;
        Vector3 pos3 = this.transform.position + (Vector3)ceilingBoxPos;
        Gizmos.DrawWireCube(pos3, ceilingBoxSize);
    }
}
