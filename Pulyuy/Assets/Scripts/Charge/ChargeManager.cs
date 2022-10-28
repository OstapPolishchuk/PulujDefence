using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeManager : MonoBehaviour
{
    int charge = 1, maxCharge = 20, minCharge = 1, amplifier = 40;
    float maxW = 775f, minW = 1f;
    [SerializeField]RectTransform chargeScale;

    void Start()
    {
        chargeScale.sizeDelta = new Vector2(minW, 28f);
    }

    void Update()
    {
       if(minW * charge * amplifier)
       chargeScale.sizeDelta = new Vector2(minW * charge * amplifier, 28f);
    }
}
