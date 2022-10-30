using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Sprite skeleton;
    [SerializeField] private Sprite[] sprites;

    [HideInInspector] public bool finished = false;

    public bool isRightSide;

    public void Init(float offset)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(Move(offset));
        isRightSide = offset > 0;
        renderer.sprite = skeleton;
    }

    public void GoToDoor()
    {
        StartCoroutine(Move(isRightSide ? 5 : -5));
    }

    public IEnumerator Move(float offset)
    {
        float timer = 3f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + new Vector3(offset, 0, 0);

        while (timer > 0)
        {
            if (PlayerMovement.finished) break;
            timer -= Time.deltaTime;
            float lerpFacor = 1f - (timer / 3f);
            transform.position = Vector3.Lerp(startPos, targetPos, lerpFacor);

            yield return null;
        }

        if (finished)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            StopCoroutine("TurnAround");
        }
        else
        {
            StartCoroutine(TurnAround());
        }
        if (!finished) finished = true;
    }

    private IEnumerator TurnAround()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public void ChangeSprite()
    {
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
