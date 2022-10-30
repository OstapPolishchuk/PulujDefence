using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayMachine : MonoBehaviour
{
    ChargeManager chargeManager;

    public int hp = 20, maxHP = 20, chargeToComp;
    public bool damaging = false, healing = false;
    public GameObject windowBlocker, hPBarParent, hPBar;
    private Enemy breakingEnemy;

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
        bool prevActive = windowBlocker.active;
        if(chargeManager.charge != chargeToComp)
        {
            if(chargeManager.charge >= 10f && hp >= 10)
            {
                windowBlocker.SetActive(false);
                if (prevActive) SoundManager.SetXray(true);
            }
            else
            {
                windowBlocker.SetActive(true);
                if (!prevActive) SoundManager.SetXray(true);
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
        if (hp <= 0) breakingEnemy.isBreaking = false;
    }

    public void StartDamaging()
    {
        StopAllCoroutines();
        StartCoroutine(Damage());
    }

    IEnumerator Damage()
    {
        while (damaging && hp > 0)
        {
            hp--;
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    IEnumerator Healing()
    {
        while(healing && hp < maxHP)
        {
            hp++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healing = false;
            damaging = true;
            if (!breakingEnemy) breakingEnemy = collision.gameObject.GetComponent<Enemy>();
            breakingEnemy.isBreaking = true;
            StartDamaging();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healing = true;
            damaging = false;
            StartCoroutine(Healing());
        }
    }
}
