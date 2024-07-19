using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Vector2 flashDir;
    // Update is called once per frame
    void Update()
    {
        rotateFlash();
    }

    void rotateFlash()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = new Vector2(transform.position.x,transform.position.y);

        flashDir = mousePos - pos;

        float angle = Mathf.Atan2(flashDir.y, flashDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
