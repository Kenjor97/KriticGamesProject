using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    public bool fullscreen = false;

    public void Resolution(int res)
    {
        if (res == 1) Screen.SetResolution(1920, 1080, fullscreen);
        if (res == 2) Screen.SetResolution(1280, 720, fullscreen);
        if (res == 3) Screen.SetResolution(800, 600, fullscreen);
    }
    public void Quality(int qua)
    {
        QualitySettings.SetQualityLevel(qua);
    }
}
