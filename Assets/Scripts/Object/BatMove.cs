using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BatMove : MonoBehaviour
{
    [SerializeField]
    List<Transform> patrollPoint; //��ǥ��ġ

    [SerializeField]
    float speed = 2f;

    private Vector2 targetPos; // ��ǥ����
    private Vector2 previousPos; //����� ����Ʈ �����ϱ�

    //public float rotate_offset = 90;

    // Start is called before the first frame update
    void Start()
    {
        if(patrollPoint.Count> 0)
        {
            //�ʱ� ��ǥ
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

            transform.rotation = Quaternion.Euler(0, transform.position.x < targetPos.x ? 0 : 180, -83); // Ÿ�� ��ġ�� ���� ���� ��ȯ
            //��ǥ��ġ�� ��� �̵�
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        
            // ��ǥ��ġ ����
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
            Debug.Log("�� �� ������!");

        }
    }
}
