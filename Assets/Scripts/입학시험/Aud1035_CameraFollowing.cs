using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_CameraFollowing : MonoBehaviour
{
    public GameObject player;
    public float offset = 0;// 카메라의 위치 조절
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraPos = new Vector3(player.transform.position.x + offset, player.transform.position.y,transform.position.z);
        transform.position = cameraPos;
    }
}
