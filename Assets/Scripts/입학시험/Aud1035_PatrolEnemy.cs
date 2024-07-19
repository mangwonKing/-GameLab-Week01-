using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_PatrolEnemy : MonoBehaviour
{
    public float speed = 3f;
    public float xLimit = 4f;
    public Transform headTransform;

    private bool isStop = false;
    private bool isRight = true;
    private bool movingRight = true;
    private Vector3 spawnPos;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            Stop();
            return; // 아무것도 하지 않기
        }
        DecisionDirection();
        Move();
    }
    void Move() //이동
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
    void DecisionDirection() //좌 , 우 방향 결정
    {
        
        if (transform.position.x > spawnPos.x + xLimit) //좌측으로 가야됨
        {
            movingRight = false;
            if(isRight)
            {
                Flip();
                isRight = false;
            }
        }
        else if (transform.position.x < spawnPos.x - xLimit) //우측으로 가야됨
        {
            movingRight = true;
            if(!isRight)
            {
                Flip();
                isRight = true;
            }
        }
    }
    void Flip()
    {
        Vector2 scaler = headTransform.localScale;
        scaler.x *= -1;
        headTransform.localScale = scaler;
    }
    void Stop()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        //rb.isknematic = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Light"))
        {
            isStop = true;
            Debug.Log("얼음!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            isStop = false;
            Debug.Log("땡!");
        }
    }
}
