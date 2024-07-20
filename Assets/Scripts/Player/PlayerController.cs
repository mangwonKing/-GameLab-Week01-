using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    float jumpPower;

    [SerializeField]
    GameObject flashLight; // �ķ��� ��

    [SerializeField]
    float flashCoolTime;

    [SerializeField]
    Vector3 sliderOffset;

    [SerializeField]
    float crouchHeight; //���̰� �Ͼ �� ������ų pos;

    public bool canOn = true;//���� �� �� �ִ��� Ȯ���Ѵ�.

    float playerDir; //�̵�����
    float originSpeed;


    bool isJump;
    bool isOn = false;// ���� ������ Ȯ���Ѵ�.
    bool isCrouch = false;

   // bool endBattery = false; //���͸��� ������ Ȯ���Ѵ�.

    Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        isJump = false;
        playerRb = GetComponent<Rigidbody2D>();
        originSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Crouch();
        TurnLight();
    }

    void Move()
    {
        playerDir = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(playerDir, 0, 0) * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            Jump();
        }

    }
    void Jump()
    {
        playerRb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    void TurnLight() //���콺 Ŭ������
    {
        if (Input.GetMouseButtonDown(0) && canOn)
        {
            LightOn_Off();
        }
    }
    void LightOn_Off()
    {
        if (isOn) 
        { 
            canOn = false;
            StartCoroutine(flashOnTimer());
        } //�� �� ��Ÿ�� ����
        isOn = !isOn; // ���� �ݴ��
        flashLight.SetActive(isOn);
    }
    public void setIsOn() //���� ��ȯ
    {
        isOn = !isOn;
    }
    public void setCanOn()
    {
        canOn = !canOn;
    }
    IEnumerator flashOnTimer()
    {
        yield return new WaitForSeconds(flashCoolTime);
        canOn = true;
    }
    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector3 originHeight = transform.GetChild(0).transform.localScale;
            Vector3 originPos =transform.localPosition;
            if (!isCrouch)
            {
                originHeight.y = originHeight.y / 2;// ���̱� -> ���� ������
                transform.GetChild(0).transform.localScale = originHeight;

                originPos.y -= crouchHeight;
                transform.localPosition = originPos;
                isCrouch = true;
            }
            else
            {
                originHeight.y = originHeight.y * 2;
                transform.GetChild(0).transform.localScale = originHeight;
                originPos.y += crouchHeight;
                transform.localPosition = originPos;
                isCrouch = false;
            }
        }
        
    }

    public void SetSpeedDown(float change)
    {
        speed /= change;
    }
    public void SetSpeedUp(float change)
    {
        speed *= change;
    }
    public void SetSpeedZero()
    {
        speed = 0;
    }
    public void SetSpeedOri()
    {
        speed = originSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJump = false;
    }
}
