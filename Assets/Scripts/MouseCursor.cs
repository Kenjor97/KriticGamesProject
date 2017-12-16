using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        HideCursor();
    }

    // Update is called once per frame
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
}
