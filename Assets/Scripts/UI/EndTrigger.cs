using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timeManager.SetGameRunning();
            timeManager.SetGameEnd();
            Debug.Log("Ż��!!!");
            Debug.Log("Ż��ð� : " + timeManager.elapsedTime);
            //���� ����ȭ�� �ҷ�����
        }
    }
}
