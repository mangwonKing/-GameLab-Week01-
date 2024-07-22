using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Light : MonoBehaviour
{
    [SerializeField]
    float maxBattery;
    //[SerializeField]
    float battery;
    [SerializeField]
    float warnningBattery; 
    [SerializeField]
    float warnningTime;

    

    Color originColor;
    float originAlpha;


    public float batteryPersentage;
    bool isWarn = false;

    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // 성능상으로 좋다.
        //Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        battery = maxBattery;
        batteryPersentage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        checkOn(); //후레쉬 켬 
        //CalculatePersent(); //퍼센트 최신화 -> 배터리 바로 이동
        CheckBattery();
        

    }
    void checkOn()
    {
        if(gameObject.activeSelf )//;
        {
            battery = Mathf.Max(0,battery -Time.deltaTime);
            //ui 이벤트 공부
            //Debug.Log(battery + "아아아");
        }
    }
    void CheckBattery()
    {
        if(battery == 0)
        //if(Mathf.Approximately(0,battery))
        {
            playerController.setCanOn();
            playerController.setIsOn(); 
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
    public float CalculatePersent()
    {
        batteryPersentage = battery / maxBattery;
        //Debug.Log(batteryPersentage);
        return batteryPersentage;
        

    }
    public void ChargeBattery() //빛을 받았다!
    {
        battery = Mathf.Min(battery + Time.deltaTime, maxBattery);
        //Debug.Log(batteryPersentage + "퍼센트 충전됨");
        if(!playerController.canOn)
        {
            playerController.canOn = true;
        }
    }
    public void SetPersentage(float per)
    {
        batteryPersentage = per;
    }
    IEnumerator Warnning()
    {
        yield return new WaitForSeconds(warnningTime);
        originColor.a = originAlpha;
        spriteRenderer.color = originColor;

        //Debug.Log("경고!");
    }

}
