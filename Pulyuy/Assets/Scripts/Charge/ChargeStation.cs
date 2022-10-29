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
        if(playerMovement.currentPos == 1)
        {
            if(!chargeManager.beingCharged && chargeManager.charge < chargeManager.maxCharge)
                btnInstruction.SetActive(true);
            else
                btnInstruction.SetActive(false);

            if(Input.GetKey(KeyCode.Space) && chargeManager.charge < chargeManager.maxCharge)
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
