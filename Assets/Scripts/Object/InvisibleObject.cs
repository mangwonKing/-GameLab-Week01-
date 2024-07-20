using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour
{
    [SerializeField]
    float invisibleSec;
    [SerializeField]
    float visibleSec;

    Coroutine currentCoroutine;

    float alpha = 0;
    SpriteRenderer spriteRenderer;
    Color initColor;
    bool isFound = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initColor = GetComponent<SpriteRenderer>().color;
        initColor.a = alpha;
        GetComponent<SpriteRenderer>().color = initColor;
    }

    // Update is called once per frame
    void Update()
    {
        StateChange();
        //Debug.Log(alpha);
    }

    void StateChange()// 발견됨을 검사
    {
        if (isFound) //보이게
        {
            if(currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(Fade(spriteRenderer.color.a, 1));
           
        }

        else //안보이게
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(Fade(spriteRenderer.color.a, 0));
        }
    }
    
    IEnumerator Fade(float start, float end)
    {
        float t = 0f;

        while (t < invisibleSec)
        {
            t += Time.deltaTime;
            if(isFound) // 밝아지는
                alpha = Mathf.Lerp(start, end, t / visibleSec);
            else //어두워지는
                alpha = Mathf.Lerp(start, end, t / invisibleSec);
            initColor.a = alpha;
            spriteRenderer.color = initColor;
            yield return null;
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Light"))
        {
            isFound = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            isFound = false; 
        }
    
    }
}
