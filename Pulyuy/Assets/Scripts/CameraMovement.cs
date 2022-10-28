using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    PlayerMovement playerMovement;
    int prevPos, curPos; 
    float middleX = 0f;
    Transform[] plrPos;
    [SerializeField]private Transform[] camPos;

    void Start()
    {
        playerMovement = PlayerMovement.instance;
        plrPos = new Transform[4];
        for(int i = 0; i < playerMovement.playerPositions.Length; i++)
        {
            plrPos[i] = playerMovement.playerPositions[i];
        }
    }

    void Update()
    {
        if(playerMovement.currentPos != playerMovement.previousPos)
        {
            curPos = playerMovement.currentPos;
            prevPos = playerMovement.previousPos;
            NewCamX();
        }
    }

    void NewCamX()
    {
        switch(curPos)
        {
            case 0:
                middleX = camPos[0].position.x;
                break;
            case 1: case 2:
                middleX = camPos[1].position.x;
                break;
            case 3:
                middleX = camPos[2].position.x;
                break;
        }

        transform.position = new Vector3(middleX, transform.position.y, transform.position.z);
    }
}
