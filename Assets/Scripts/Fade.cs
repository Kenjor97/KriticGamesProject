﻿using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    public FadeOutScript fadeOut;
    public GameObject fadeInGameObject;
    public Texture2D fadeInTexture;    // the texture that will overlay the screen. This can be a black image or a loading graphic
    public float fadeSpeed = 0.8f;      // the fading speed
    public GameObject fadeOutObejct;
    private int drawDepth = -1000;      // the texture's order in the draw hierarchy: a low number means it renders on top
    public float alpha = 0.0f;         // the texture's alpha value between 0 and 1
    private int fadeDir = 1;           // the direction to fade: in = -1 or out = 1

    void OnGUI()
    {
        // fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // force (clamp) the number to be between 0 and 1 because GUI.color uses Alpha values between 0 and 1
        alpha = Mathf.Clamp01(alpha);

        // set color of our GUI (in this case our texture). All color values remain the same & the Alpha is set to the alpha variable
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;                                                              // make the black texture render on top (drawn last)
        GUI.DrawTexture(new Rect(Screen.width/3, Screen.height/6, fadeInTexture.width/3, fadeInTexture.height/3), fadeInTexture);       // draw the texture to fit the entire screen area
    }

    // sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    // OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a parameter so you can limit the fade in to certain scenes.
    void OnLevelWasLoaded()
    {
        // alpha = 1;		// use this if the alpha is not set to 1 by default
        BeginFade(-1);      // call the fade in function

    }


    private void Update()
    {
        if (alpha == 1.0f)
        {
            fadeInGameObject.SetActive(false);
            fadeOut.Activate();
        }
    }


}
