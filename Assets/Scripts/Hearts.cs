using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public int heartHeal;

    void Start()
    {
        heartHeal = 1;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D Event " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log(collision);
            collision.GetComponent<CharacterBehaviour>().RestoreLife(heartHeal);
            Destroy(this.gameObject);
        }
    }
}
