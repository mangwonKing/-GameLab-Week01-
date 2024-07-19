using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_PlayerController : MonoBehaviour
{
    //이동 및 점프
    public float speed = 5f;
    public float jumpPower = 5f;

    //대쉬
    public float dashSpeed = 10f;
    public float dashTime = 0.2f;
    public float dashCooldown = 2f;

    // 빛
    public GameObject flashLight;

    // 점프대 추력
    public float schanzePower = 20f;

    //private
    private Transform headTransform; //머리 방향을 위한 멤버
    private Rigidbody2D playerRb;
    private GameObject flash; // 손전등
    private Aud1035_GameManager gm;
    private GameObject dashEffect;

    private float moveInput = 0;
    private bool isJump = false;
    private bool isRight = true; //우측을 보고있는지
    private bool isDashing = false;// 대쉬중인지
    private bool turnLight = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        flash = GameObject.Find("Flash Slot");
        headTransform = GameObject.Find("Head").GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponentInParent<Aud1035_GameManager>();
        dashEffect = GameObject.Find("ReadyDash");
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isGameover)
        {
            return;
        }
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            Jump();
        }
        if (moveInput > 0 && !isRight)
        {
            Flip();
        }
        else if(moveInput < 0 && isRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) 
        {
            StartCoroutine(Dash());
        }
        RotateFlashToMouse();
        if (Input.GetKeyDown(KeyCode.F))
        {
            LightOn_Off();
        }
    }
    void Move() //좌,우 이동
    {
        moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * speed * moveInput * Time.deltaTime);
    }
    void Flip() //좌 , 우 바라보기
    {
        isRight = !isRight;
        Vector2 scaler = headTransform.localScale;
        scaler.x *= -1;
        headTransform.localScale = scaler;
    }
    void Jump()
    {
        isJump = true;
        playerRb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    IEnumerator Dash()
    {
       
        isDashing = true;
        dashEffect.SetActive(!isDashing); //대쉬준비 이펙트 제거
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0;
        playerRb.velocity = new Vector2(moveInput * transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashTime);
        playerRb.gravityScale = originalGravity;
        playerRb.velocity = new Vector2(0, 0);//추력 제거하기

        yield return new WaitForSeconds(dashCooldown - dashTime);
        isDashing = false;
        dashEffect.SetActive(!isDashing);//대쉬 준비 이펙트on
        Debug.Log("대쉬 가능!"); //log 찍기 , 빌드시 제거하기?
    }
    void RotateFlashToMouse()
    {
        //마우스 위치 월드좌표로
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //플래쉬 슬롯 위치를 vec2로 
        Vector2 flashPos = flash.transform.position;
        //방향 계산
        Vector2 direction = mousePos - flashPos;
        //회전각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        flash.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    void LightOn_Off()
    {
        turnLight = !turnLight; //turn 의 상태를 반전
        flashLight.SetActive(turnLight);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ground"))
            || collision.gameObject.CompareTag("Changeable"))
        {
            foreach (ContactPoint2D contact in collision.contacts)//윗면에서만 점프하도록
            {
                if (Vector2.Angle(contact.normal, Vector2.up) < 45)
                {
                    isJump = false;
                }
            }
            //isJump = false;
        }
        if(collision.gameObject.CompareTag("Schanze"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (Vector2.Angle(contact.normal, Vector2.up) < 45)
                {
                    playerRb.velocity = Vector2.up * schanzePower;
                }
            }
        }
        if (collision.gameObject.CompareTag("Egg")) //알을 먹으면
        {
            gm.AcquireEgg();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gm.TakeDamaged();
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
            gm.GameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Goal") && gameObject.CompareTag("Player"))
        {
            gm.CheckGameClear();
        }
    }
}
