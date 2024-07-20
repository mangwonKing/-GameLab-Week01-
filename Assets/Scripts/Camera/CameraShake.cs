using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float amount;
    [SerializeField]
    float duration;

    Vector3 originPos;


    void Start()
    {
        originPos = transform.localPosition;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Shake());
        }
    }
    public IEnumerator Shake()
    {
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
