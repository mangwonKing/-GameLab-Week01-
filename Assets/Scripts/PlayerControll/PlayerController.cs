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

    public bool canOn = true;//���� �� �� �ִ��� Ȯ���Ѵ�.

    float playerDir; //�̵�����
    
    bool isJump;
    bool isOn = false;// ���� ������ Ȯ���Ѵ�.
    
    bool endBattery = false; //���͸��� ������ Ȯ���Ѵ�.

    Rigidbody2D playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        isJump = false;
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
    IEnumerator flashOnTimer()
    {
        yield return new WaitForSeconds(flashCoolTime);
        canOn = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJump = false;
    }
}
