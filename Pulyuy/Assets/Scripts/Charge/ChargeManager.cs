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

    EnemiesManager enemiesManager;

    int minCharge = 1, amplifier = 40, killPerExh = 4; 
    public int charge = 1, maxCharge = 20;
    float maxW = 800f, minW = 40f;
    public bool beingCharged = false, exhausted = false;
    bool disChargeStarted = false, helpingChargeBool = false, exhaustWorkedOnce = false;

    [SerializeField]Canvas chargeCanvas;
    [SerializeField]RectTransform chargeScale;
    [SerializeField]TextMeshProUGUI percentage;
    [SerializeField]Image percBckgrnd;
    
    public XRayMachine[] xrMachines;

    void Start()
    {
        enemiesManager = EnemiesManager.instance;

        chargeScale.sizeDelta = new Vector2(minW, 28f);
        chargeCanvas.GetComponent<Canvas>().enabled = true;
    }

    void Update()
    {
        if(charge * amplifier <= maxW)
            chargeScale.sizeDelta = new Vector2(charge * amplifier, 28f);
        
        //Charging/Discharging
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

        //Exhausting
        if(charge == maxCharge && !exhaustWorkedOnce)
        {
            exhausted = true;
            Invoke("TurnOffExhausted", 5f);
            charge = minCharge;

            percentage.text = "--";
            Explode();
            exhaustWorkedOnce = true;
        }

        if(!beingCharged)
            helpingChargeBool = false;
        
        if(charge == minCharge || beingCharged)
            disChargeStarted = false;

        for(int i = 0; i < xrMachines.Length; i++)
           xrMachines[i].WindowsBlockerManager();
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

    //Discharging charge as time goes by
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

    //Charging charge as player holds Space near generetor
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

    //Exploding X-rays as charge = 100%
    void Explode()
    {
        Enemy enemyToKill = null;
        for (int i = 0; i < killPerExh / 2; i++)
        {
            if (enemiesManager.enemiesR.Count - 1 >= 0)
            {
                enemyToKill = enemiesManager.enemiesR[0];
                enemiesManager.enemiesR.RemoveAt(0);
                enemyToKill.Die();
                enemiesManager.OffsetAllRight();
            }

            if (enemiesManager.enemiesL.Count - 1 >= 0)
            {
                enemyToKill = enemiesManager.enemiesL[0];
                enemiesManager.enemiesL.RemoveAt(0);
                enemyToKill.Die();
                enemiesManager.OffsetAllLeft();
            }
        }
    }

    void TurnOffExhausted()
    {
        exhausted = false;
        exhaustWorkedOnce = false;
    }
}
