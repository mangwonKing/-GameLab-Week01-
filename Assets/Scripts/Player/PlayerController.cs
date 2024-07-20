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
    GameObject flashLight; // 후레쉬 빛

    [SerializeField]
    float flashCoolTime;

    [SerializeField]
    Vector3 sliderOffset;

    [SerializeField]
    float crouchHeight; //숙이고 일어날 때 변동시킬 pos;

    public bool canOn = true;//빛을 켤 수 있는지 확인한다.

    float playerDir; //이동방향
    float originSpeed;


    bool isJump;
    bool isOn = false;// 빛의 켜짐을 확인한다.
    bool isCrouch = false;

   // bool endBattery = false; //배터리의 방전을 확인한다.

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
    void TurnLight() //마우스 클릭감지
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
        } //끌 때 쿨타임 적용
        isOn = !isOn; // 상태 반대로
        flashLight.SetActive(isOn);
    }
    public void setIsOn() //상태 전환
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
                originHeight.y = originHeight.y / 2;// 숙이기 -> 절반 굽히기
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
