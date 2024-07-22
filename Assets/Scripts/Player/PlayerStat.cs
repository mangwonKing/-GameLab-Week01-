using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStat : MonoBehaviour
{
    public TextMeshProUGUI diamondScore;

    // �÷��̾� ���� ����
    [SerializeField]
    int maxHp;

    [SerializeField]
    float chageSpeed;

    [SerializeField]
    float batteryOut = 5f;

    public bool isHit = false;

    public int diamond = 0;

    Light flashLight;
    SpriteRenderer spriteRenderer;
    PlayerController playerController;

    float batSteal;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController= transform.parent.GetComponent<PlayerController>();
        flashLight = GameObject.Find("Flash").transform.GetComponentInChildren<Light>(true);//  GetChild(0).GetComponent<Light>();
        Debug.Log(GameObject.FindGameObjectWithTag("Light"));
        UpdateDiamond();
    }
    private void Update()
    {
        UpdateDiamond();
    }

    void UpdateDiamond()
    {
        diamondScore.text = "Diamond: " + diamond;
    }
    //bool waterHit = false;
    //bool stoneHit = false;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            Color originColor = spriteRenderer.color;
            spriteRenderer.color = Color.blue;
            StartCoroutine(HitColorChage(originColor)); // ���� ����

            playerController.SetSpeedDown(chageSpeed);
            StartCoroutine(SpeedChange());
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Color originColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            StartCoroutine(HitColorChage(originColor)); // ���� ����
            Debug.Log("���� ���͸� : " + flashLight.GetBattery());
            flashLight.SetBatteryDown(batteryOut);
            Debug.Log("���� �� ���͸� : " + flashLight.GetBattery());
            playerController.SetSpeedZero();
            StartCoroutine(SpeedChangeObst());

            isHit = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isHit)// 
        {
            isHit = true;
            Color originColor = spriteRenderer.color;
            spriteRenderer.color = Color.gray;
            StartCoroutine(HitColorChage(originColor)); // ���� ����
            //Debug.Log("���� ���͸� : " + flashLight.GetBattery());
            //flashLight.SetBatteryDown(batteryOut);
            //Debug.Log("���� �� ���͸� : " + flashLight.GetBattery());
            batSteal = Random.Range(1, 4); // 1~3�� ��������
            diamond = Mathf.Max(diamond - 1, 0);
            Debug.Log("���̾ƺ��� ����, ���� ���̾� = " + diamond);
            playerController.SetSpeedZero();
            StartCoroutine(SpeedChangeObst());
        }
        if (collision.gameObject.CompareTag("Diamond") )
        {
            //�߰� ����Ʈ ������ ������

            diamond += Random.Range(1, 3); // �Ӹ��� 1~2���� �򵵷�
            Debug.Log("���̾� �߰�! " + diamond + "��");
            Destroy(collision.gameObject);
        }

    }

    IEnumerator HitColorChage(Color origin)
    {
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = origin;
        isHit = false;
        //Debug.Log("�� �������!");

    }
    IEnumerator SpeedChange()
    {
        yield return new WaitForSeconds(0.25f);
        playerController.SetSpeedUp(chageSpeed);
    }
    IEnumerator SpeedChangeObst()
    {
        yield return new WaitForSeconds(0.25f);
        playerController.SetSpeedOri();
    }
}
