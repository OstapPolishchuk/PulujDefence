using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public static CraftManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("instance freak up");
        }
        instance = this;
    }

    PlayerMovement playerMovement;

    [SerializeField] Canvas bulletCanvas;
    [SerializeField] GameObject[] bulletsImages;
    [SerializeField] GameObject btnInstruction;
    public int bullets;
    int maxbullets = 3, minbullets = 0, bulletsToComp;
    bool crafting = false, helpingCraftBool = false;

    void Start()
    {
        playerMovement = PlayerMovement.instance;

        bulletCanvas.GetComponent<Canvas>().enabled = true;
        bulletsToComp = bullets;
    }

    void Update()
    {
        if(crafting && !helpingCraftBool)
        {
            StopAllCoroutines();
            StartCoroutine(Craft());
        }

        if(!crafting)
        {
            helpingCraftBool = false;
        }
        UpdateBulletsList();
        DetectNearby();
    }

    IEnumerator Craft()
    {
        helpingCraftBool = true;
        while(crafting && bullets < maxbullets)
        {
            yield return new WaitForSeconds(2f);
            bullets++;
        }
    }

    void DetectNearby()
    {
        if(playerMovement.currentPos == 2)
        {
            btnInstruction.SetActive(true);
            if(Input.GetKey(KeyCode.Space))
                crafting = true;
            else
                crafting = false;
        }
        else
        {
            btnInstruction.SetActive(false);
            crafting = false;
        }
    }

    void UpdateBulletsList()
    {
        if(bulletsToComp != bullets)
        {
            for(int i = 0; i < bullets; i++)
            {
                bulletsImages[i].SetActive(true);
            }
            for(int i = bullets; i < bulletsImages.Length; i++)
            {
                bulletsImages[i].SetActive(false);
            }
        }

        bulletsToComp = bullets;
    }
}
