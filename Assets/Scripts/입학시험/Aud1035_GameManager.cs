using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Aud1035_GameManager : MonoBehaviour
{
    public TextMeshProUGUI eggTxt;
    public TextMeshProUGUI lifeTxt;
    public GameObject[] eggs;
    
    public Button restartButton;
    public GameObject gameOverScreen;
    public GameObject ingameUI;

    public GameObject gameClearScreen;
    public Button nextStageButton;

    public GameObject endGameScreen;
    public int egg = 0;
    public int needEgg = 0;

    public int life = 3;
    public int maxLife = 3;

    public bool isGameover = false;
    private bool isGameClear = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
       needEgg = eggs.Length;
    }

    // Update is called once per frame
    void Update()
    {
        TextOn();
    }
    void TextOn()
    {
        eggTxt.text = "Egg: " + egg + " / " + needEgg;
        lifeTxt.text = "Life: " + life + " / " + maxLife;
    }
    public void AcquireEgg()
    {
        egg++;
        if(egg > needEgg)egg = needEgg;
    }
    public void TakeDamaged()
    {
        life--;
        if (life <= 0)// 게임오버
        {
            life = 0;
            GameOver();
        }
    }
    public void GameOver()
    {
        isGameover = true;
        gameOverScreen.SetActive(isGameover);
        ingameUI.SetActive(!isGameover); //인게임 ui끄기
        Time.timeScale = 0;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CheckGameClear()
    {
        if(needEgg == egg)isGameClear = true;
        if (isGameClear)
        {
            int curSceneIdx = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIdx = curSceneIdx + 1;
            if (nextSceneIdx == SceneManager.sceneCountInBuildSettings)
            {
                endGameScreen.SetActive(true);
                Time.timeScale = 0f;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit(); //종료
                }
            }
            else
            {
                gameClearScreen.SetActive(isGameClear);
                ingameUI.SetActive(!isGameover); //인게임 ui끄기
                Time.timeScale = 0;
            }
        }

    }
    public void LoadNextStage()
    {
        int curSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = curSceneIdx + 1;

        if(nextSceneIdx < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIdx);
        }
    }
}
