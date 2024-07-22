using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeBar : MonoBehaviour
{
    [SerializeField]
    PlayerStat playerStat;

    Color originColor;

    Image bar;
    // Update is called once per frame
    private void Start()
    {
        bar = GetComponent<Image>();
        originColor = bar.color;
    }
    void Update()
    {
        if(playerStat.isHit)
        {
            HitObjectColor();
        }
    }
    void HitObjectColor()
    {
        bar.color = Color.red;
        StartCoroutine(ChangedColor());
    }
    IEnumerator ChangedColor()
    {
        yield return new WaitForSeconds(0.25f);
        bar.color = originColor;
    }
}
