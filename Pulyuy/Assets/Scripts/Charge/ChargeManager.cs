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
    float maxW = 800f, minW = 40f, chargeToComp;
    public bool beingCharged = false, exhausted = false;
    bool disChargeStarted = false, helpingChargeBool = false, exhaustWorkedOnce = false;

    [SerializeField]Canvas chargeCanvas;
    [SerializeField]RectTransform chargeScale;
    [SerializeField]TextMeshProUGUI percentage;
    [SerializeField]Image percBckgrnd;

    [SerializeField]GameObject[] windowBlockers;

    void Start()
    {
        chargeScale.sizeDelta = new Vector2(minW, 28f);
        chargeCanvas.GetComponent<Canvas>().enabled = true;
        chargeToComp = charge;
    }

    void Update()
    {
        if(charge * amplifier <= maxW)
            chargeScale.sizeDelta = new Vector2(charge * amplifier, 28f);
        
        if(!exhausted)
        {
            UpdatePercentage();

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
        }

        if(charge == maxCharge && !exhaustWorkedOnce)
        {
            exhausted = true;
            charge = minCharge;

            percentage.text = "--";
            Invoke("TurnOffExhausted", 5f);
            exhaustWorkedOnce = true;
        }

        if(!beingCharged)
            helpingChargeBool = false;
        
        if(charge == minCharge || beingCharged)
            disChargeStarted = false;

        WindowsBlockerManager();
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

    void TurnOffExhausted()
    {
        exhausted = false;
        exhaustWorkedOnce = false;
    }

    void WindowsBlockerManager()
    {
        if(charge != chargeToComp)
        {
            if(charge >= 10f)
            {
                for(int i = 0; i < windowBlockers.Length; i++)
                {
                    windowBlockers[i].SetActive(false);
                }
            }
            else
            {
                for(int i = 0; i < windowBlockers.Length; i++)
                {
                    windowBlockers[i].SetActive(true);
                }
            }
        }
        chargeToComp = charge;
    }
}
