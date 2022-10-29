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

    int charge = 1, maxCharge = 20, minCharge = 1, amplifier = 40, killPerExh = 4;
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
        enemiesManager = EnemiesManager.instance;

        chargeScale.sizeDelta = new Vector2(minW, 28f);
        chargeCanvas.GetComponent<Canvas>().enabled = true;
        chargeToComp = charge;
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
        for(int i = 0; i < killPerExh; i++)
        {
            if(enemiesManager.enemies[i])
            {
                enemyToKill = enemiesManager.enemies[i];
                if(enemyToKill)
                {
                    if(enemyToKill.isRightSide && enemiesManager.countR - 1 >= 0)
                    {
                        enemiesManager.countR--;
                    }
                    else if(!enemyToKill.isRightSide && enemiesManager.countL - 1 >= 0)
                    {
                        enemiesManager.countL--;
                    }
                    enemyToKill.Die();
                }
            }
        }

        for(int i = 0; i < enemiesManager.enemies.Count; i++)
        {
            if(enemiesManager.enemies[i] == null)
                enemiesManager.enemies.RemoveAt(i);
        }
    }

    void TurnOffExhausted()
    {
        exhausted = false;
        exhaustWorkedOnce = false;
    }

    //Turning on or off windows blockers to see enemies if charge is >50%
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
