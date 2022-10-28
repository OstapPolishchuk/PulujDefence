using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPositions;
    [SerializeField] private GameObject player;
    bool canMove = true, locked = false;
    int previousPos, currentPos;
    float nextMoveTime, moveRate;

    void Start()
    {
        currentPos = 2;
        previousPos = currentPos;
        nextMoveTime = 0f;
        moveRate = 1.2f;
    }

    void Update()
    {
        if(Time.time >= nextMoveTime && locked)
        {
            nextMoveTime = Time.time + 1f / moveRate;
            locked = false;
        }
        
        if(canMove && !locked)
        {
            Movement();
        }
    }

    void Movement()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 && (currentPos + (int)Input.GetAxisRaw("Horizontal")) >= 0)
        {
            if((currentPos + (int)Input.GetAxisRaw("Horizontal")) <= 4)
            {
                currentPos = currentPos + (int)Input.GetAxisRaw("Horizontal");
                player.transform.position = playerPositions[currentPos].transform.position;
                locked = true;
            }
        }

    }

    void FixedUpdate()
    {
        float Offset = currentPos - previousPos;
    }
}
