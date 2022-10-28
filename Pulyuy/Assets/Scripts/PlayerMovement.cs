using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Instance freak up");
        }
        instance = this;
    }

    public Transform[] playerPositions;
    [SerializeField] private GameObject player;
    bool canMove = true, locked = false;
    public int previousPos, currentPos, offset;
    float nextMoveTime, moveRate;

    void Start()
    {
        for(int i = 0; i < playerPositions.Length; i++)
        {
            if(player.transform.position.x == playerPositions[i].position.x)
                currentPos = i;
        }
        
        previousPos = currentPos;
        offset = currentPos;
        moveRate = 1f;
    }

    void Update()
    {
        if(canMove && !locked)
        {
            Movement();
            player.transform.position = playerPositions[currentPos].position;
        }

        if(offset != currentPos)
        {
            previousPos = offset;
        }
        offset = currentPos;
    }

    void Movement()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 && (currentPos + (int)Input.GetAxisRaw("Horizontal")) >= 0)
        {
            if((currentPos + (int)Input.GetAxisRaw("Horizontal")) < 4)
            {
                if(Input.GetAxisRaw("Horizontal") > 0f)
                {
                    currentPos++;
                    locked = true;
                    Invoke("TurnOffLocked", moveRate);
                }

                else if(Input.GetAxisRaw("Horizontal") < 0f)
                {
                    currentPos--;
                    locked = true;
                    Invoke("TurnOffLocked", moveRate);
                }
                locked = true;
            }
        }
    }

    void TurnOffLocked()
    {
        locked = false;
    }
}
