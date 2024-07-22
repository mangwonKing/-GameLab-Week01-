using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; // ���� �ð��� ǥ���� �ؽ�Ʈ
    public float elapsedTime; // ��� �ð�
    private bool isGameRunning; // ���� ���� ������ ����

    bool isGameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� �� ��� �ð� �ʱ�ȭ
        elapsedTime = 0f;
        isGameRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("�Ͻ�����!");
            SetGameRunning();
        }
        if (isGameRunning && !isGameEnd)
        {
            // �� �����Ӹ��� ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            // ��� �ð��� UI �ؽ�Ʈ�� ǥ��
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime % 60F);
            timeText.text = string.Format("Time: {0:0}:{1:00}", minutes, seconds);
        }
    }
    // ���� ���� �� ȣ���� �޼���
    public void EndGame()
    {
        isGameRunning = false;

        // ��� �ð� ����
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
        PlayerPrefs.Save();
    }

    // ���� ���� �� ��� �ð� �ҷ�����
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
