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

        GameObject go = GameObject.Find("Flash"); //�̸������Ұ�
        Debug.Log(go.name);
        playerLight = go.transform.GetComponentInChildren<Light>(true);
        Debug.Log(playerLight == null);
        //Debug.Log("�÷��̾� ����Ʈ �̸� : " + playerLight.name);
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
            Debug.Log("�������� ����");
            InPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("��������Ż��");
            InPlayer = false;
        }
    }

}
