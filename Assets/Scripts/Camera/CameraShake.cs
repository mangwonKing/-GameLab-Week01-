using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public static CameraShake instance;
    [SerializeField]
    float amount;
    [SerializeField]
    float duration;

    Vector3 originPos;


    void Start()
    {
        originPos = transform.localPosition;
    }
    public void OnShake()
    {
        StartCoroutine(Shake());
    }
    public IEnumerator Shake()
    {
        //Debug.Log("카메라 흔들어!");
        float timer = 0;
        while (timer <= duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;

    }
}
