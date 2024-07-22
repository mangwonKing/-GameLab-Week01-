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
            Debug.Log("탈출!!!");
            Debug.Log("탈출시간 : " + timeManager.elapsedTime);
            //게임 종료화면 불러오기
        }
    }
}
