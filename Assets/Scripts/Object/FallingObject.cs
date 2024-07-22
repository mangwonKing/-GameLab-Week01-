using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    GameObject Obj;

    [SerializeField]
    float fallingCycle; // ������ �� ����Ŭ


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
            
            //Debug.Log(fallingTime + "�� �� �������ϴ�.");
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
