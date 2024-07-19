using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_PlayerController : MonoBehaviour
{
    //�̵� �� ����
    public float speed = 5f;
    public float jumpPower = 5f;

    //�뽬
    public float dashSpeed = 10f;
    public float dashTime = 0.2f;
    public float dashCooldown = 2f;

    // ��
    public GameObject flashLight;

    // ������ �߷�
    public float schanzePower = 20f;

    //private
    private Transform headTransform; //�Ӹ� ������ ���� ���
    private Rigidbody2D playerRb;
    private GameObject flash; // ������
    private Aud1035_GameManager gm;
    private GameObject dashEffect;

    private float moveInput = 0;
    private bool isJump = false;
    private bool isRight = true; //������ �����ִ���
    private bool isDashing = false;// �뽬������
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
    void Move() //��,�� �̵�
    {
        moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * speed * moveInput * Time.deltaTime);
    }
    void Flip() //�� , �� �ٶ󺸱�
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
        dashEffect.SetActive(!isDashing); //�뽬�غ� ����Ʈ ����
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0;
        playerRb.velocity = new Vector2(moveInput * transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashTime);
        playerRb.gravityScale = originalGravity;
        playerRb.velocity = new Vector2(0, 0);//�߷� �����ϱ�

        yield return new WaitForSeconds(dashCooldown - dashTime);
        isDashing = false;
        dashEffect.SetActive(!isDashing);//�뽬 �غ� ����Ʈon
        Debug.Log("�뽬 ����!"); //log ��� , ����� �����ϱ�?
    }
    void RotateFlashToMouse()
    {
        //���콺 ��ġ ������ǥ��
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //�÷��� ���� ��ġ�� vec2�� 
        Vector2 flashPos = flash.transform.position;
        //���� ���
        Vector2 direction = mousePos - flashPos;
        //ȸ������ ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        flash.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    void LightOn_Off()
    {
        turnLight = !turnLight; //turn �� ���¸� ����
        flashLight.SetActive(turnLight);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ground"))
            || collision.gameObject.CompareTag("Changeable"))
        {
            foreach (ContactPoint2D contact in collision.contacts)//���鿡���� �����ϵ���
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
        if (collision.gameObject.CompareTag("Egg")) //���� ������
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
