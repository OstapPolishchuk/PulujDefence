using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Init(Enemy enemy)
    {
        transform.LookAt(enemy.transform);
        transform.Rotate(0, 0, 90);
        StartCoroutine(Flight(enemy));
    }

    private IEnumerator Flight(Enemy enemy)
    {
        float timer = 0.3f;
        Vector3 startPos = transform.position;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float lerpFacor = 1f - (timer / 0.3f);
            transform.position = Vector3.Lerp(startPos, enemy.transform.position + new Vector3(0, 1, -2), lerpFacor);

            yield return null;
        }
        if (enemy.isRightSide)
        {
            EnemiesManager.instance.enemiesR.Remove(enemy);
            EnemiesManager.instance.OffsetAllRight();
        }
        else
        {
            EnemiesManager.instance.enemiesL.Remove(enemy);
            EnemiesManager.instance.OffsetAllLeft();
        }

        enemy.Die();
        LevelManager.instance.CloseLeft();
        LevelManager.instance.CloseRight();
        Destroy(gameObject);
    }
}