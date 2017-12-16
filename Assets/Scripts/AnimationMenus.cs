using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMenus : MonoBehaviour
{
    Animator Table;
    Animation WoodTable;

	// Use this for initialization
	void Start ()
    {
        
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
        WoodTable.Stop();
            Debug.Log("Pulsado P");
        }
        
        
	}


}
