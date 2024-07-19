using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Slider BatteryBar;
   
    void SetPower(float power)
    {
        BatteryBar.value = power;
    }
}
