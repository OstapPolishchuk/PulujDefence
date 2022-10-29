using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeStation : MonoBehaviour
{
    PlayerMovement playerMovement;
    ChargeManager chargeManager;
    [SerializeField]GameObject btnInstruction;

    void Start()
    {
        playerMovement = PlayerMovement.instance;
        chargeManager = ChargeManager.instance;
    }

    void Update()
    {
        DetectNearGenerator();
    }

    void DetectNearGenerator()
    {
        Debug.Log(Input.GetKey(KeyCode.Space));
        if(playerMovement.currentPos == 1)
        {
            btnInstruction.SetActive(true);
            if(Input.GetKey(KeyCode.Space))
                chargeManager.beingCharged = true;
            else
                chargeManager.beingCharged = false;
        }
        else
        {
            btnInstruction.SetActive(false);
            chargeManager.beingCharged = false;
        }
    }
}
