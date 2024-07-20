using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalaciteFall : MonoBehaviour
{
    [SerializeField]
    GameObject Stone;
    bool isFalling = false;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(!isFalling)
        {
            isFalling = true;
            StartCoroutine(FallingStone());
        }
    }

    IEnumerator FallingStone()
    {
        yield return new WaitForSeconds(3f);
        isFalling=false;
        Instantiate(Stone,transform.position,transform.rotation);
       
       
    }
}
