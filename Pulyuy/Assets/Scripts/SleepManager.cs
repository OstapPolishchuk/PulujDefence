using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    ChargeManager chargeManager;

    Vector3 lastPlrPos;
    bool stage1Started, stage2Started;
    float checkRate, sleepTimer;
    [SerializeField] Canvas sleepCanvas;
    [SerializeField] GameObject stage1Img, stage2Img;

    void Start()
    {
        playerMovement = PlayerMovement.instance;
        chargeManager = ChargeManager.instance;

        stage1Started = false;
        stage2Started = false;
        sleepTimer = 0f;
        checkRate = 1f/4f;
        lastPlrPos = playerMovement.transform.position;
    }

    void Update()
    {
        //Sleep timer add every frame
        sleepTimer += Time.deltaTime;

        //Checking if player displaced, if yes, setting sleepTimer to 0
        if(playerMovement.transform.position != lastPlrPos || (chargeManager.beingCharged && !chargeManager.exhausted))
        {
            sleepTimer = 0f;
            ReturnToAwake();
        }
        
        //Checking if sleepTimer is more than 3 seconds, if yes start falling asleep and setting sleepTimer to 0
        if(sleepTimer >= 1f / checkRate)
        { 
            FallAsleep();
            sleepTimer = 0f;
        }

        lastPlrPos = playerMovement.transform.position;
    }

    void FallAsleep()
    {
        if(!stage1Started)
        {
            Stage1Sleep();
        }   
        else if(stage1Started && !stage2Started)
        {
            Stage2Sleep();
        }
        else if(stage1Started && stage2Started)
        {
            playerMovement.Die();
        }
    }

    void Stage1Sleep()
    {
        stage1Started = true;

        sleepCanvas.GetComponent<Canvas>().enabled = true;
        stage1Img.SetActive(true);
        stage2Img.SetActive(false);
    }
    
    void Stage2Sleep()
    {
        stage2Started = true;

        sleepCanvas.GetComponent<Canvas>().enabled = true;
        stage1Img.SetActive(false);
        stage2Img.SetActive(true);
    }

    void ReturnToAwake()
    {
        stage1Started = false;
        stage2Started = false;

        sleepCanvas.GetComponent<Canvas>().enabled = false;
        stage1Img.SetActive(false);
        stage2Img.SetActive(false);
    }
}
