using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalacite : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Light"))
        {
            Destroy(this.gameObject);
        }
        
    }
}