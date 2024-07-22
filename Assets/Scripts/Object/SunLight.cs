using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLight : MonoBehaviour
{
    bool InPlayer = false;

    Light playerLight;
    

    private void Start()
    {
        //playerLight = GameObject.Find("Flash").transform.GetComponentInChildren<Light>(true);

        GameObject go = GameObject.Find("Flash"); //이름주의할것
        Debug.Log(go.name);
        playerLight = go.transform.GetComponentInChildren<Light>(true);
        Debug.Log(playerLight == null);
        //Debug.Log("플레이어 라이트 이름 : " + playerLight.name);
    }
    void Update()
    {
        if(InPlayer)
        {
            playerLight.ChargeBattery();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충전지역 도착");
            InPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충전지역탈출");
            InPlayer = false;
        }
    }

}
