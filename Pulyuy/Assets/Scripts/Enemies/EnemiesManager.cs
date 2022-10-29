using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance;

    [SerializeField] private GameObject enemyPrefab;
    [Space]
    [SerializeField] private Transform leftWindow;
    [SerializeField] private Transform rightWindow;

    private int countL;
    private int countR;

    private const float spawningInterval = 10f;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("instance freak up");
        }
        instance = this;
        StartCoroutine(Spawning());
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private IEnumerator Spawning()
    { 
        while(true)
        {
            Debug.Log(countL + "  " + countR);
            Enemy enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 20), Quaternion.Euler(0, 0, 0)).GetComponent<Enemy>();
            int a = Random.Range(0, 2);
            if (a == 0)
            {
                if (countL < 3)
                {
                    enemy.Init(leftWindow.position.x + countL - 1);
                    countL++;
                }
                else if (countR < 3)
                {
                    enemy.Init(rightWindow.position.x - countR + 1);
                    countR++;
                }
                else
                {
                    // Knock;
                }
            }
            else
            {
                if (countR < 3)
                {
                    enemy.Init(rightWindow.position.x - countR + 1);
                    countR++;
                }
                else if (countL < 3)
                {
                    enemy.Init(leftWindow.position.x + countL - 1);
                    countL++;
                }
                else
                {
                    // Knock;
                }
            }

            yield return new WaitForSeconds(spawningInterval);
        }
    }
}
