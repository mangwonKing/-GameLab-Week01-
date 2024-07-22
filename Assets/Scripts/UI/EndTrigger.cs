using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;

    [SerializeField]
    TextMeshProUGUI gameClearText; 
    [SerializeField]
    TextMeshProUGUI rankText;
    [SerializeField]
    TextMeshProUGUI diaText;
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    Button restartButton;

    [SerializeField]
    Button endButton;


    [SerializeField]
    PlayerStat playerStat;
    [SerializeField]
    PlayerController playerController;

    string rank = "C";
    string diaRanktxt = "C";
    string timeRanktxt = "C";

    int total = 2;
    int diaRank = 1;
    int timeRank = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timeManager.SetGameRunning();
            timeManager.SetGameEnd();
            //Debug.Log("탈출!!!");
            Debug.Log("탈출시간 : " + timeManager.elapsedTime);

            CaculateRank();
            //게임 종료화면 불러오기
            gameClearText.gameObject.SetActive(true);
            rankText.gameObject.SetActive(true);
            diaText.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            endButton.gameObject.SetActive(true);

            gameClearText.text = "GameClear!!";
            rankText.text = "Total Rank :" + rank;
            diaText.text = "Diamond :" + diaRanktxt;
            timeText.text = "Time :" + timeRanktxt;
            Time.timeScale = 0;


        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        playerController.isRestart = true;
        
    }
    public void EndGame()
    {
        Application.Quit();
        Debug.Log("게임 종료!");
    }
    void CaculateRank()
    {
        if(timeManager.elapsedTime < 360) //6분 이하
        {
            timeRank = 3;
            timeRanktxt = "A";
        }
        else if(timeManager.elapsedTime >=360 && timeManager.elapsedTime <480)
        {
            timeRank= 2;
            timeRanktxt = "B";
        }
        else
        {
            timeRank = 1;
            timeRanktxt = "C";
        }
        if(playerStat.diamond >= 75)
        {
            diaRank = 3;
            diaRanktxt = "A";
        }
        else if(playerStat.diamond < 75 && playerStat.diamond >= 30)
        {
            diaRank = 2;
            diaRanktxt = "B";
        }
        else
        {
            diaRank = 1;
            diaRanktxt = "C";
        }

        total = diaRank + timeRank;
        if (total == 6)
            rank = "S";
        else if (total < 6 && total > 4)
            rank = "A";
        else if(total <= 4 && total > 2)
            rank = "B";
        else
            rank = "C";
        
    }
}
