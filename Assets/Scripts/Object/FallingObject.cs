using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    GameObject Obj;
    bool isFalling = false;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (!isFalling)
        {
            isFalling = true;
            StartCoroutine(FallingObj());
        }
    }

    IEnumerator FallingObj()
    {
        yield return new WaitForSeconds(3f);
        isFalling = false;
        Instantiate(Obj, transform.position, transform.rotation);


    }
}
