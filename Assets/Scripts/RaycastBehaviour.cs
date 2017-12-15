using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehaviour : MonoBehaviour
{
    public bool isColliding = false;

    [Header("Ray properties")]
    public Vector2 origin;
    public Vector2 direction;
    public ContactFilter2D filter;
    public float distance;

    void Start ()
    {

	}
	
	void MyFixedUpdate ()
    {
        Vector3 pos = this.transform.position + (Vector3)origin;
        RaycastHit2D[] results = new RaycastHit2D[1];

        int numColliders = Physics2D.Raycast(pos, direction, filter, results, distance);
        if(numColliders > 0)
        {
            isColliding = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = this.transform.position + (Vector3)origin;
        Gizmos.DrawRay(pos, direction);
    }
}
