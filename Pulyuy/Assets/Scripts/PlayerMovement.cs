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
    [HideInInspector()]public int previousPos, currentPos, offset;
    float nextMoveTime, moveRate, startZ = -.1f;

    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private FinishMenu finishMenu;

    void Start()
    {
        for(int i = 0; i < playerPositions.Length; i++)
        {
            if(player.transform.position.x == playerPositions[i].position.x)
                currentPos = i;
        }
        
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, startZ);
        previousPos = currentPos;
        offset = currentPos;
        moveRate = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.transform.position.x <= -12 || player.transform.position.x >= 12) Shoot();
        }
        if(canMove && !locked)
        {
            Movement();
            player.transform.position = new Vector2(playerPositions[currentPos].position.x, player.transform.position.y);
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

        //Flipping player
        if (transform.position.x <= -12)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
        }
        else if (transform.position.x >= 12)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
        }
        else
        {
            if (previousPos > currentPos)
            {
                //player.GetComponent<SpriteRenderer>().flipX = true;
                transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            }
            else if (previousPos < currentPos)
            {
                //player.GetComponent<SpriteRenderer>().flipX = false;
                transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
            }
        }
    }

    void TurnOffLocked()
    {
        locked = false;
    }

    public void Die()
    {
        Debug.Log("Oh! The misery!");
        finishMenu.gameObject.SetActive(true);
        finishMenu.IsSuccesfullyCompletes(false);
        SoundManager.Finish(false);
    }

    public void Win()
    {
        finishMenu.gameObject.SetActive(true);
        finishMenu.IsSuccesfullyCompletes(true);
        SoundManager.Finish(true);
    }

    private void Shoot()
    {
        if (CraftManager.instance.bullets >= 1)
        {
            Enemy enemy = null;

            if (transform.position.x > 0)
            {
                if (EnemiesManager.instance.enemiesR.Count >= 1)
                {
                    enemy = EnemiesManager.instance.enemiesR[0];
                    EnemiesManager.instance.enemiesR.RemoveAt(0);
                    LevelManager.instance.OpenRight();
                }
            }
            else
            {
                if (EnemiesManager.instance.enemiesL.Count >= 1)
                {
                    enemy = EnemiesManager.instance.enemiesL[0];
                    EnemiesManager.instance.enemiesL.RemoveAt(0);
                    LevelManager.instance.OpenLeft();
                }
            }

            if (!enemy)
            {
                LevelManager.instance.CloseLeft();
                LevelManager.instance.CloseRight();
                return;
            }

            SoundManager.PlayShoot();
            CraftManager.instance.bullets--;
            Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation).GetComponent<Bullet>().Init(enemy);
        }
    }
}
