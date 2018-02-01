using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransformEasingExam : MonoBehaviour
{
    public enum Value { POSITION, ROTATION, SCALE, COLOR }
    public Value value;
    public enum Type { BACK, BOUNCE, CIRC, CUBIC, ELASTIC, EXPO, SINE, QUAD, QUART, QUINT }
    public Type type;

    public Vector3 iniValue;
    public Vector3 finalValue;
    Vector3 deltaValue;

    public float currentTime;
    public float timeDuration;
    public float Delay;

    public bool pinpong;
    public bool restart;


    // Use this for initialization
    void Start()
    {
        deltaValue = finalValue - iniValue;
        currentTime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(Delay > 0)
        {
            Delay -= Time.deltaTime;
            return;
        }

        if(currentTime <= timeDuration)
        {
            Vector3 easingValue = new Vector3();
            switch(type)
            {
                case Type.BACK:
                    easingValue = new Vector3(
                       Easing.BackEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.BackEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.BackEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.BOUNCE:
                    easingValue = new Vector3(
                       Easing.BounceEaseOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.BounceEaseOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.BounceEaseOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.CIRC:
                    easingValue = new Vector3(
                       Easing.CircEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.CircEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.CircEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.CUBIC:
                    easingValue = new Vector3(
                       Easing.CubicEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.CubicEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.CubicEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.ELASTIC:
                    easingValue = new Vector3(
                       Easing.ElasticEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.ElasticEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.ElasticEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.EXPO:
                    easingValue = new Vector3(
                       Easing.ExpoEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.ExpoEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.ExpoEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.SINE:
                    easingValue = new Vector3(
                       Easing.SineEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.SineEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.SineEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.QUAD:
                    easingValue = new Vector3(
                       Easing.QuadEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.QuadEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.QuadEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.QUART:
                    easingValue = new Vector3(
                       Easing.QuartEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.QuartEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.QuartEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                case Type.QUINT:
                    easingValue = new Vector3(
                       Easing.QuintEaseInOut(currentTime, iniValue.x, deltaValue.x, timeDuration),
                       Easing.QuintEaseInOut(currentTime, iniValue.y, deltaValue.y, timeDuration),
                       Easing.QuintEaseInOut(currentTime, iniValue.z, deltaValue.z, timeDuration));
                    break;
                default:
                    break;
            }

            switch(value)
            {
                case Value.POSITION:
                    transform.localPosition = easingValue;
                    break;
                case Value.ROTATION:
                    transform.localRotation = Quaternion.Euler(easingValue);
                    break;
                case Value.SCALE:
                    transform.localScale = easingValue;
                    break;
                case Value.COLOR:
                    
                    break;
                default:
                    break;
            }

            currentTime += Time.deltaTime;

            if(currentTime > timeDuration)
            {
                switch(value)
                {
                    case Value.POSITION:
                        transform.localPosition = finalValue;
                        break;
                    case Value.ROTATION:
                        transform.localRotation = Quaternion.Euler(finalValue);
                        break;
                    case Value.SCALE:
                        transform.localScale = finalValue;
                        break;
                    default:
                        break;
                }

                Debug.Log("El easing ha acabado");

                if(restart) currentTime = 0;

                else if(pinpong)
                {
                    currentTime = 0;
                    Vector3 ini = iniValue;
                    iniValue = finalValue;
                    finalValue = ini;
                    deltaValue = finalValue - iniValue;
                }
            }
        }

        else
        {
            Debug.Log("El easing hace un rato que ha acabado");
        }
    }

}
