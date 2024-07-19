using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_IncreaseXObject : MonoBehaviour
{
    public float increaseSpeed = 2f; // ���� �޾� �����ϴ� �ӵ�
    public float decreaseSpeed = 0.25f; // �ڵ����� �پ��� �ӵ�

    public float maxScaleX = 10f;
    public float minScaleX = 1f;

    private bool canIncrease = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //���� ������ �� ��������
        Vector2 currentScale = transform.localScale;

        // ���� ��ġ �� ��������
        Vector2 currentPosition = transform.position;
        if (canIncrease)
        {
            Increase(currentScale, currentPosition);
        }
        else if (!canIncrease)
        {
            Decrease(currentScale, currentPosition);
        }
    }
    void Increase(Vector2 curScale, Vector2 curPos)
    {
        if (curScale.x < maxScaleX)
        {
            //y�� ����
            float scaleIncrement = increaseSpeed * Time.deltaTime;
            curScale.x = Mathf.Min(curScale.x + scaleIncrement, maxScaleX);
            //��ġ ����
            curPos.x += scaleIncrement / 2;
            // ����
            transform.localScale = curScale;
            transform.position = curPos;
        }
    }
    void Decrease(Vector2 curScale, Vector2 curPos)
    {
        if (curScale.x > minScaleX)
        {
            //y�� ����
            float scaleDecrement = decreaseSpeed * Time.deltaTime;
            curScale.x = Mathf.Max(curScale.x - scaleDecrement, minScaleX);
            //��ġ ����
            curPos.x -= scaleDecrement / 2;
            // ����
            transform.localScale = curScale;
            transform.position = curPos;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            canIncrease = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            canIncrease = false;
        }

    }
}
