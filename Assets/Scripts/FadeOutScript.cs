using UnityEngine;
using System.Collections;

public class FadeOutScript : MonoBehaviour
{

    public Texture2D fadeOutTexture;    // the texture that will overlay the screen. This can be a black image or a loading graphic
    public float fadeSpeedOut = 0.8f;      // the fading speed

    private int drawDepthOut = -1000;      // the texture's order in the draw hierarchy: a low number means it renders on top
    private float alphaOut = 1.0f;         // the texture's alpha value between 0 and 1
    private int fadeDirOut = -1;           // the direction to fade: in = -1 or out = 1

    void OnGUI()
    {
        // fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
        alphaOut += fadeDirOut * fadeSpeedOut * Time.deltaTime;
        // force (clamp) the number to be between 0 and 1 because GUI.color uses Alpha values between 0 and 1
        alphaOut = Mathf.Clamp01(alphaOut);

        // set color of our GUI (in this case our texture). All color values remain the same & the Alpha is set to the alpha variable
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alphaOut);
        GUI.depth = drawDepthOut;                                                              // make the black texture render on top (drawn last)
        GUI.DrawTexture(new Rect(Screen.width / 3, 0, fadeOutTexture.width / 3, fadeOutTexture.height / 3), fadeOutTexture);       // draw the texture to fit the entire screen area
    }

    // sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
    public float BeginFade(int direction)
    {
        fadeDirOut = direction;
        return (fadeSpeedOut);
    }

    // OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a parameter so you can limit the fade in to certain scenes.
    void OnLevelWasLoaded()
    {
        // alpha = 1;		// use this if the alpha is not set to 1 by default
        BeginFade(-1);      // call the fade in function

    }


}
