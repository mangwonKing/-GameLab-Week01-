using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_RotateObject : MonoBehaviour
{
    public float rotateSpeed = 5f;
    private float backUpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        backUpSpeed = rotateSpeed;// 스피드 백업
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed *Time.deltaTime);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Light"))
        {
            rotateSpeed = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            rotateSpeed = backUpSpeed;
        }
    }
}
