using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime;
    public Vector2 offSet = Vector2.zero;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        Vector3 newPosition = target.position + new Vector3(offSet.x, offSet.y, -15);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
}
