using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alpha : MonoBehaviour
{

    public float initValue;
    public float endValue;

//    public Image titleText; 
    public float desiredTime;
    float currentTime;
    public float delay;

	void Update () {


		if(delay >= 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        currentTime += Time.deltaTime;

        if(currentTime <= desiredTime)
        {
            float tempValue;

            tempValue = Easing.QuartEaseOut(currentTime, initValue, endValue - initValue, desiredTime);

            this.gameObject.GetComponent<Image>().color = new Vector4(this.gameObject.GetComponent<Image>().color.r, this.gameObject.GetComponent<Image>().color.g, this.gameObject.GetComponent<Image>().color.b, tempValue);
        }

	}
}
