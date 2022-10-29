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

    public List<Enemy> enemies = new List<Enemy>();

    public int countL, countR;

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
            Debug.Log(countL);
            Debug.Log(countR);
            Enemy enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 20), Quaternion.Euler(0, 0, 0)).GetComponent<Enemy>();
            int a = Random.Range(0, 2);
            if (a == 0)
            {
                if (countL < 3) SpawnLeft(enemy);
                else if (countR < 3) SpawnRight(enemy);
                else
                {
                    Knock();
                }
            }
            else
            {
                if (countR < 3) SpawnRight(enemy);
                else if (countL < 3)  SpawnLeft(enemy);
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
        enemy.Init(rightWindow.position.x - countR + 1);
        countR++;
        enemies.Add(enemy);
    }

    private void SpawnLeft(Enemy enemy) 
    {
        enemy.Init(leftWindow.position.x + countL - 1);
        countL++;
        enemies.Add(enemy);
    }

    private void Knock()
    {
        // foreach (Enemy _enemy in enemies) _enemy.GoToDoor();
    }
}
