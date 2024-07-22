using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    GameObject Obj;

    [SerializeField]
    float fallingCycle; // 기준이 될 사이클


    float fallingTime;

    bool isFalling = false;
    // Start is called before the first frame update
    [SerializeField]
    bool isStalacite = false;

    CameraShake cameraShake;
    // Update is called once per frame
    private void Start()
    {
        //cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    void Update()
    {
        if (!isFalling)
        {
            isFalling = true;
            if (isStalacite)
            {
                cameraShake.OnShake();
            }
            
            //Debug.Log(fallingTime + "초 뒤 떨어집니다.");
            StartCoroutine(FallingObj());
        }
    }

    IEnumerator FallingObj()
    {
        yield return new WaitForSeconds(fallingCycle);
        isFalling = false;
        Instantiate(Obj, transform.position, transform.rotation);


    }
}
