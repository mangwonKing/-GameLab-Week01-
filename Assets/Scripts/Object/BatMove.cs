using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BatMove : MonoBehaviour
{
    [SerializeField]
    List<Transform> patrollPoint; //목표위치

    [SerializeField]
    float speed = 2f;

    private Vector2 targetPos; // 목표지점
    private Vector2 previousPos; //출발한 포인트 저장하기

    //public float rotate_offset = 90;

    // Start is called before the first frame update
    void Start()
    {
        if(patrollPoint.Count> 0)
        {
            //초기 목표
            targetPos = patrollPoint[Random.Range(0,patrollPoint.Count)].position;
            previousPos = targetPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(patrollPoint.Count > 0)
        {
            float degree =Vector2.Angle(transform.position, targetPos);

            transform.rotation = Quaternion.Euler(0, transform.position.x < targetPos.x ? 0 : 180, -83); // 타겟 위치에 따라 방향 전환
            //목표위치로 계속 이동
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        
            // 목표위치 도달
            if(Vector2.Distance(transform.position, targetPos)<0.1f)
            {
                previousPos = targetPos;
                targetPos = patrollPoint[Random.Range(0,patrollPoint.Count)].position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            targetPos = previousPos;
            Debug.Log("악 빛 무서워!");

        }
    }
}
