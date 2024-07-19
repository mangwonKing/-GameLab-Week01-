using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud1035_ObjectSpawn : MonoBehaviour
{
    public GameObject spawnObj;
    public float spawnTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn() //2段原陥 持失
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(spawnObj,transform.position,transform.rotation);
        }
    }
}
