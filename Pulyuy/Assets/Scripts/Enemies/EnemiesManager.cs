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

    public List<Enemy> enemiesL = new List<Enemy>();
    public List<Enemy> enemiesR = new List<Enemy>();

    private const float spawningInterval = 5f;

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

    private IEnumerator Spawning()
    { 
        while(true)
        {
            Debug.Log(enemiesL.Count);
            Debug.Log(enemiesR.Count);
            Enemy enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 20), Quaternion.Euler(0, 0, 0)).GetComponent<Enemy>();
            int a = Random.Range(0, 2);
            if (a == 0)
            {
                if (enemiesL.Count < 3) SpawnLeft(enemy);
                else if (enemiesR.Count < 3) SpawnRight(enemy);
                else
                {
                    Knock();
                }
            }
            else
            {
                if (enemiesR.Count < 3) SpawnRight(enemy);
                else if (enemiesL.Count < 3)  SpawnLeft(enemy);
                else
                {
                    Knock();
                }
            }

            yield return new WaitForSeconds(spawningInterval);
        }
    }

    private void SpawnRight(Enemy enemy) 
    {
        enemy.Init(rightWindow.position.x - enemiesR.Count + 1);
        enemiesR.Add(enemy);
    }

    private void SpawnLeft(Enemy enemy) 
    {
        enemy.Init(leftWindow.position.x + enemiesL.Count - 1);
        enemiesL.Add(enemy);
    }

    private void Knock()
    {
        // foreach (Enemy _enemy in enemies) _enemy.GoToDoor();
    }
}
