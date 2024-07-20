using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{


    // 플레이어 상태 변수
    [SerializeField]
    int maxHp;

    [SerializeField]
    float chageSpeed;

    int hp;
   

    SpriteRenderer spriteRenderer;
    PlayerController playerController;
    private void Start()
    {
        hp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController= transform.parent.GetComponent<PlayerController>();
    }


    //bool waterHit = false;
    //bool stoneHit = false;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            Color originColor = spriteRenderer.color;
            spriteRenderer.color = Color.blue;
            StartCoroutine(HitColorChage(originColor)); // 색상 변경

            playerController.SetSpeedDown(chageSpeed);
            StartCoroutine(SpeedChange());
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Color originColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            StartCoroutine(HitColorChage(originColor)); // 색상 변경
            hp = Mathf.Max(hp - 1, 0); // 체력 깎기
            Debug.Log("체력 : " + hp);

            playerController.SetSpeedZero();
            StartCoroutine(SpeedChangeObst());


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Color originColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            StartCoroutine(HitColorChage(originColor)); // 색상 변경
            hp = Mathf.Max(hp - 1, 0); // 체력 깎기
            Debug.Log("체력 : " + hp);

            playerController.SetSpeedZero();
            StartCoroutine(SpeedChangeObst());
        }
    }
    IEnumerator HitColorChage(Color origin)
    {
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = origin;
        //Debug.Log("색 원래대로!");

    }
    IEnumerator SpeedChange()
    {
        yield return new WaitForSeconds(0.25f);
        playerController.SetSpeedUp(chageSpeed);
    }
    IEnumerator SpeedChangeObst()
    {
        yield return new WaitForSeconds(0.25f);
        playerController.SetSpeedOri();
    }
}
