using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Instance freak up");
        }
        instance = this;
    }

    [SerializeField] private SpriteRenderer leftDoor;
    [SerializeField] private SpriteRenderer rightDoor;
    [Space]
    [SerializeField] private Sprite opened;
    [SerializeField] private Sprite closed;

    void Start()
    {
        //CloseLeft();
        //CloseRight();
    }

    public void OpenLeft() 
    { 
        leftDoor.sprite = opened;
        SoundManager.SetDoor(true);
    }

    public void CloseLeft() 
    { 
        leftDoor.sprite = closed;
        SoundManager.SetDoor(false);
    }

    public void OpenRight() 
    {
        rightDoor.sprite = opened;
        SoundManager.SetDoor(true);
    }

    public void CloseRight() 
    {
        rightDoor.sprite = closed;
        SoundManager.SetDoor(false);
    }
}