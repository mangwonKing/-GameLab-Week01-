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

    float dir;
    
    bool isJump;
    bool isOn = false;
    bool canOn = true;

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
        dir = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(dir, 0, 0) * speed * Time.deltaTime);
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
