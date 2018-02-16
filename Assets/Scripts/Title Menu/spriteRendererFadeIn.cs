using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteRendererFadeIn : MonoBehaviour {

    public float initValue;
    public float endValue;

    public float desiredTime;
    float currentTime;
    public float delay;

    void Update()
    {


        if (delay >= 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        currentTime += Time.deltaTime;

        if (currentTime <= desiredTime)
        {
            float tempValue;

            tempValue = Easing.QuartEaseOut(currentTime, initValue, initValue - endValue, desiredTime);

            this.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(this.gameObject.GetComponent<SpriteRenderer>().color.r, this.gameObject.GetComponent<SpriteRenderer>().color.g, this.gameObject.GetComponent<SpriteRenderer>().color.b, tempValue);
        }
        if( currentTime >= desiredTime)
        {
            Debug.Log("El Fade ha acabado");
        }
    }
}