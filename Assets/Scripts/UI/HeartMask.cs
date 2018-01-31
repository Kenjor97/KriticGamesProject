using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMask : MonoBehaviour
{
    public CharacterBehaviour player;
    public RectTransform rt;
    public float fourHearts;
    public float fiveHearts;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
        rt = GetComponent<RectTransform>();
	}
	
	void Update ()
    {
		if(player.maxLife == 4)
        {
            rt.sizeDelta = new Vector2(fourHearts, rt.sizeDelta.y);
        }
        if(player.maxLife == 5)
        {
            rt.sizeDelta = new Vector2(fiveHearts, rt.sizeDelta.y);
        }
    }
}
