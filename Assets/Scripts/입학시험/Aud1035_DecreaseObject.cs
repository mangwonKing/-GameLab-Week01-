using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_DecreaseObject : MonoBehaviour
{
    public float increaseSpeed = 0.25f; // 자동으로 증가하는 속도
    public float decreaseSpeed = 2f; // 빛을 받아 감소하는 속도

    public float maxScaleY = 10f;
    public float minScaleY = 1f;

    private bool canDecrease = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //현재 스케일 값 가져오기
        Vector2 currentScale = transform.localScale;

        // 현재 위치 값 가져오기
        Vector2 currentPosition = transform.position;
        if(canDecrease)
        {
            Decrease(currentScale, currentPosition);
        }
        else if(!canDecrease)
        {
            Increase(currentScale, currentPosition);
        }
    }

    void Decrease(Vector2 curScale,Vector2 curPos)
    {
        if (curScale.y > minScaleY)
        {
            //y축 감소
            float scaleDecrement = decreaseSpeed * Time.deltaTime;
            curScale.y = Mathf.Max(curScale.y - scaleDecrement, minScaleY);
            //위치 조정
            curPos.y -= scaleDecrement / 2;
            // 적용
            transform.localScale = curScale;
            transform.position = curPos;

        }
    }
    void Increase(Vector2 curScale, Vector2 curPos)
    {
        if (curScale.y < maxScaleY)
        {
            //y축 증가
            float scaleIncrement = increaseSpeed * Time.deltaTime;
            curScale.y = Mathf.Min(curScale.y + scaleIncrement, maxScaleY);
            //위치 조정
            curPos.y += scaleIncrement / 2;
            // 적용
            transform.localScale = curScale;
            transform.position = curPos;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
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
