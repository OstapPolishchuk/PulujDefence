using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Sprite[] sprites;

    private static int count;

    public void Init(float offset)
    {
        StartCoroutine(Move(offset));
        renderer.sprite = sprites[count];
        //renderer.sprite = sprites[Random.Range(0, sprites.Length)];
        count++;
    }

    void Update()
    {
        
    }

    private IEnumerator Move(float offset)
    {
        float timer = 5f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + new Vector3(offset, 0, 0);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float lerpFacor = 1f - (timer / 5f);
            transform.position = Vector3.Lerp(startPos, targetPos, lerpFacor);

            yield return null;
        }
        StartCoroutine(TurnAround());
    }

    private IEnumerator TurnAround()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
