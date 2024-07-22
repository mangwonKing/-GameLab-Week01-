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
    [SerializeField]
    float chargeTime = 1f;
    

    Color originColor;
    float originAlpha;


    public float batteryPersentage;

    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // ���ɻ����� ����.
        
        //Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        battery = maxBattery;
        batteryPersentage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        checkOn(); //�ķ��� �� 
        //CalculatePersent(); //�ۼ�Ʈ �ֽ�ȭ -> ���͸� �ٷ� �̵�
        CheckBattery();
        

    }
    void checkOn()
    {
        if(gameObject.activeSelf )//;
        {
            battery = Mathf.Max(0,battery -Time.deltaTime);
            //ui �̺�Ʈ ����
            //Debug.Log(battery + "�ƾƾ�");
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

            Debug.Log("�ķ��� ���͸� ����!");
        }
        
    }
    public float CalculatePersent()
    {
        batteryPersentage = battery / maxBattery;
        //Debug.Log(batteryPersentage);
        return batteryPersentage;
        

    }
    public void ChargeBattery() //���� �޾Ҵ�!
    {
        battery = Mathf.Min(battery + Time.deltaTime * chargeTime, maxBattery);
        //Debug.Log(batteryPersentage + "�ۼ�Ʈ ������");
        if(!playerController.canOn)
        {
            playerController.canOn = true;
        }
    }
    public void SetPersentage(float per)
    {
        batteryPersentage = per;
    }
    public void SetBatteryDown(float n)
    {
        battery = Mathf.Max(battery - n, 0);
    }
    public float GetBattery()
    {
        return battery;
    }
    IEnumerator Warnning()
    {
        yield return new WaitForSeconds(warnningTime);
        originColor.a = originAlpha;
        spriteRenderer.color = originColor;

        //Debug.Log("���!");
    }

}
