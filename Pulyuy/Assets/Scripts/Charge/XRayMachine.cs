using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayMachine : MonoBehaviour
{
    ChargeManager chargeManager;

    public int hp = 20, maxHP = 20, chargeToComp;
    public bool damaging = false, healing = false;
    public GameObject windowBlocker, hPBarParent, hPBar;

    void Start()
    {
        chargeManager = ChargeManager.instance;
        chargeToComp = chargeManager.charge;
    }

    void Update()
    {
        UpdateHPBar();
    }
    
    //Turning on or off windows blockers to see enemies if charge is >50%
    public void WindowsBlockerManager()
    {
        if(chargeManager.charge != chargeToComp)
        {
            if(chargeManager.charge >= 10f && hp >= 10)
            {
                windowBlocker.SetActive(false);
            }
            else
            {
                windowBlocker.SetActive(true);
            }
        }
        chargeToComp = chargeManager.charge;
    }

    void UpdateHPBar()
    {
        if(hp != maxHP)
        {
            hPBarParent.SetActive(true);
            hPBar.transform.localScale = new Vector2(1/20f * hp, hPBar.transform.localScale.y);
        }
        else
        {
            hPBarParent.SetActive(false);
        }
    }

    public void StartDamaging()
    {
        StopAllCoroutines();
        StartCoroutine(Damage());
    }

    IEnumerator Damage()
    {
        while(damaging)
        {
            hp--;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator Healing()
    {
        while(healing)
        {
            hp++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
