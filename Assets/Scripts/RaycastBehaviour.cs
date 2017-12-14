using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehaviour : MonoBehaviour
{
    public float distance;
    public bool isColliding = false;
    public ContactFilter2D filter;

    void Start ()
    {
        distance = 10;
	}
	
	void Update ()
    {
        Physics2D.Raycast(this.transform.position, Vector2.up, distance);
        //int numColliders = 
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
