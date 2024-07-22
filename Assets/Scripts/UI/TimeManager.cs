using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; // 게임 시간을 표시할 텍스트
    public float elapsedTime; // 경과 시간
    private bool isGameRunning; // 게임 진행 중인지 여부

    bool isGameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        // 게임 시작 시 경과 시간 초기화
        elapsedTime = 0f;
        isGameRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("일시정지!");
            SetGameRunning();
        }
        if (isGameRunning && !isGameEnd)
        {
            // 매 프레임마다 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 경과 시간을 UI 텍스트에 표시
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime % 60F);
            timeText.text = string.Format("Time: {0:0}:{1:00}", minutes, seconds);
        }
    }
    // 게임 종료 시 호출할 메서드
    public void EndGame()
    {
        isGameRunning = false;

        // 경과 시간 저장
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
        PlayerPrefs.Save();
    }

    // 게임 종료 후 경과 시간 불러오기
    public void LoadGameTime()
    {
        if (PlayerPrefs.HasKey("ElapsedTime"))
        {
            elapsedTime = PlayerPrefs.GetFloat("ElapsedTime");
        }
    }
    public void SetGameRunning()
    {
        isGameRunning = !isGameRunning;
    }
    public void SetGameEnd()
    {
        isGameEnd = true;
    }
}
