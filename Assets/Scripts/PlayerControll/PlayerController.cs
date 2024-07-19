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

    public bool canOn = true;//빛을 켤 수 있는지 확인한다.

    float playerDir; //이동방향
    
    bool isJump;
    bool isOn = false;// 빛의 켜짐을 확인한다.
    
    bool endBattery = false; //배터리의 방전을 확인한다.

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
