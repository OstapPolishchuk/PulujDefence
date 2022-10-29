using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChargeManager : MonoBehaviour
{
    public static ChargeManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("instance freak up");
        }
        instance = this;
    }

    int charge = 1, maxCharge = 20, minCharge = 1, amplifier = 40;
    float maxW = 800f, minW = 1f;
    public bool beingCharged = false;
    bool disChargeStarted = false, helpingChargeBool = false;
    [SerializeField]RectTransform chargeScale;
    [SerializeField]TextMeshProUGUI percentage;
    [SerializeField]Image percBckgrnd;

    void Start()
    {
        chargeScale.sizeDelta = new Vector2(minW, 28f);
    }

    void Update()
    {
        UpdatePercentage();

        if(minW * charge * amplifier <= maxW)
            chargeScale.sizeDelta = new Vector2(minW * charge * amplifier, 28f);
        
        if(!beingCharged && !disChargeStarted)
        {
            StopAllCoroutines();
            StartCoroutine(DisCharge());
        }

        if(beingCharged && !helpingChargeBool)
        {
            StopAllCoroutines();
            StartCoroutine(Charge());
        }

        if(beingCharged == false)
            helpingChargeBool = false;
        
        if(charge == minCharge || beingCharged)
            disChargeStarted = false;
    }

    void UpdatePercentage()
    {
        if(charge == maxCharge)
            percentage.text = "100%";
        
        else if(charge == minCharge)
            percentage.text = "0%";

        else
            percentage.text = charge * 5 + "%";
    }

    IEnumerator DisCharge()
    {
        disChargeStarted = true;
        percBckgrnd.GetComponent<Image>().color = new Color32(255, 0, 0,255);
        while(!beingCharged && charge > minCharge)
        {
            yield return new WaitForSeconds(0.7f);
            charge--;
        }
    }

    IEnumerator Charge()
    {
        percBckgrnd.GetComponent<Image>().color = new Color32(0, 255, 0,255);
        helpingChargeBool = true;
        while(beingCharged && charge < maxCharge)
        {
            yield return new WaitForSeconds(0.8f);
            charge++;
        }
    }
}
