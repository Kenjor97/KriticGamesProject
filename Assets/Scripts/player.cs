using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("State")]
    public bool isGrounded;
    public bool wasGroundedLastFrame;
    public bool justGotGrounded;
    public bool justNotGrounded;
    public bool isFalling;

    [Header("Filter")]
    public ContactFilter2D filter;
    public int maxColliders = 1;
    public bool checkGround = true;

    [Header("Box properties")]
    public Vector2 groundBoxPos;
    public Vector2 groundBoxSize;

    private void FixedUpdate()
    {
        ResetState();
        GroundDetection();
    }

    void ResetState()
    {
        wasGroundedLastFrame = isGrounded;
        isFalling = !isGrounded;

        isGrounded = false;
        justNotGrounded = false;
        justGotGrounded = false;
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
        }
        if(!wasGroundedLastFrame && isGrounded) justGotGrounded = true;
        if(wasGroundedLastFrame && !isGrounded) justNotGrounded = true;

        if(justNotGrounded) Debug.Log("JUST NOT GROUNDED");
        if(justGotGrounded) Debug.Log("just got grounded");
    }
    void WallDetection()
    {

    }
    void CeilingDetection()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = this.transform.position + (Vector3)groundBoxPos;
        Gizmos.DrawWireCube(pos, groundBoxSize);
    }
}
