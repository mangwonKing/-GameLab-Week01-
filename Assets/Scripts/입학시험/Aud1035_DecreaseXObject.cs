using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_DecreaseXObject : MonoBehaviour
{
    public float increaseSpeed = 0.25f; // �ڵ����� �����ϴ� �ӵ�
    public float decreaseSpeed = 2f; // ���� �޾� �����ϴ� �ӵ�

    public float maxScaleX = 10f;
    public float minScaleX = 1f;

    private bool canDecrease = false;
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
        if (canDecrease)
        {
            Decrease(currentScale, currentPosition);
        }
        else if (!canDecrease)
        {
            Increase(currentScale, currentPosition);
        }
    }

    void Decrease(Vector2 curScale, Vector2 curPos)
    {
        if (curScale.x > minScaleX)
        {
            //X�� ����
            float scaleDecrement = decreaseSpeed * Time.deltaTime;
            curScale.x = Mathf.Max(curScale.x - scaleDecrement, minScaleX);
            //��ġ ����
            curPos.x -= scaleDecrement / 2;
            // ����
            transform.localScale = curScale;
            transform.position = curPos;

        }
    }
    void Increase(Vector2 curScale, Vector2 curPos)
    {
        if (curScale.x < maxScaleX)
        {
            //X�� ����
            float scaleIncrement = increaseSpeed * Time.deltaTime;
            curScale.x = Mathf.Min(curScale.x + scaleIncrement, maxScaleX);
            //��ġ ����
            curPos.x += scaleIncrement / 2;
            // ����
            transform.localScale = curScale;
            transform.position = curPos;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            Debug.Log("minus ����Ʈ ����!");
            canDecrease = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            canDecrease = false;
        }

    }
}
