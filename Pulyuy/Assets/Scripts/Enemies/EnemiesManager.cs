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

    public List<Enemy> enemiesInside = new List<Enemy>();

    private const float spawningInterval = 2f;

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
        while (true)
        {
            Enemy enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 20), Quaternion.Euler(0, 0, 0)).GetComponent<Enemy>();
            int a = Random.Range(0, 2);
            if (a == 0)
            {
                if (enemiesL.Count < 3) SpawnLeft(enemy);
                else KnockLeft();
            }
            else
            {
                if (enemiesR.Count < 3) SpawnRight(enemy);
                else KnockRight();
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

    private void KnockLeft()
    {
        Enemy enemy = enemiesL[0];
        StartCoroutine(LeftBrokeInto(enemy));
    }

    private void KnockRight()
    {
        Enemy enemy = enemiesR[0];
        StartCoroutine(RightBrokeInto(enemy));
    }

    public void OffsetAllLeft()
    {
        foreach (Enemy enemy in enemiesL) if (enemy.finished && enemy) StartCoroutine(enemy.Move(-1));
    }

    public void OffsetAllRight()
    {
        foreach (Enemy enemy in enemiesR) if (enemy.finished && enemy) StartCoroutine(enemy.Move(1));
    }

    private IEnumerator LeftBrokeInto(Enemy enemy)
    {
        
        enemy.GoToDoor();
        OffsetAllLeft();
        yield return new WaitForSeconds(3);
        if (enemy)
        {
            enemiesL.RemoveAt(0);
            enemiesInside.Add(enemy);
            enemy.ChangeSprite();
            LevelManager.instance.OpenLeft();
            enemy.transform.position = enemy.transform.position + new Vector3(0, -0.5f, -22);
            enemy.transform.localScale *= 1.2f;
            while (true)
            {
                enemy.transform.position += new Vector3(Time.deltaTime, 0, 0);
                yield return null;
            }
        }
    }

    private IEnumerator RightBrokeInto(Enemy enemy)
    {
        enemy.GoToDoor();
        OffsetAllRight();
        yield return new WaitForSeconds(3);
        if (enemy)
        {
            enemiesR.RemoveAt(0);
            enemiesInside.Add(enemy);
            enemy.ChangeSprite();
            LevelManager.instance.OpenRight();
            enemy.transform.position = enemy.transform.position + new Vector3(0, 0, -22);
            enemy.transform.localScale *= 1.2f;
            while (!enemy.killedPlayer)
            {
                enemy.transform.position -= new Vector3(Time.deltaTime, 0, 0);
                yield return null;
            }
        }
    }
}
