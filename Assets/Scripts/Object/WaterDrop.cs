using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Light"))
        {
            Destroy(this.gameObject);
        }
        //if(collision.gameObject.CompareTag("Player"))
        //{

        //}

    }

    //IEnumerator HitWater(GameObject player)
    //{
    //    player.GetComponent<PlayerController>();
    //}
}
