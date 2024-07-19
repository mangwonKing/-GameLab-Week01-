using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BatteryBar : MonoBehaviour
{
    //배터리바
    [SerializeField]
    Slider BatterySlide;

    Light flashLight;

    Transform myTransform;

    float persentageToLight;
    private void Start()
    {
        myTransform = transform.parent;
        flashLight = myTransform.Find("Flash").GetChild(0).GetComponent<Light>();
    }

    private void Update()
    {
        persentageToLight = flashLight.CalculatePersent();

        SetPower(persentageToLight);
    }
    public void SetPower(float power)
    {
        BatterySlide.value = power;
        //Debug.Log("슬라이드바에 적용되는 배터리 =" + power);
    }
}
