using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField]
    float maxBattery;
    [SerializeField]
    float battery;
    [SerializeField]
    float warnningBattery; // 퍼센트
    [SerializeField]
    float warnningTime;

    Color originColor;
    float originAlpha;

    bool isWarn = false;

    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        checkOn();
        CheckBattery();
    }
    void checkOn()
    {
        if(gameObject.activeSelf )
        {
            battery = Mathf.Max(0,battery -Time.deltaTime);
            //Debug.Log(battery);
        }
    }
    void CheckBattery()
    {
        if(battery == 0)
        //if(Mathf.Approximately(0,battery))
        {
            playerController.canOn = false; 
            gameObject.SetActive(false);
            Debug.Log("후레쉬 배터리 방전!");
        }
        if (warnningBattery > battery && !isWarn) // 경고를 했는지
        {
            isWarn = true;
            originAlpha = spriteRenderer.color.a;
            originColor.a = 0;
            spriteRenderer.color = originColor;
            StartCoroutine(Warnning());
        }
    }
    IEnumerator Warnning()
    {
        
        yield return new WaitForSeconds(warnningTime);
        originColor.a = originAlpha;
        spriteRenderer.color = originColor;

        Debug.Log("경고!");
    }
}
