using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float frameRate = 0.1f;
    [SerializeField] private float displayDuration = 0.5f;

    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && sprites.Length > 0)
        {
            StartCoroutine(PlayExplosionAnimation());
        }
    }

    private IEnumerator PlayExplosionAnimation()
    {
        float startTime = Time.time;
        for (int i = 0; i < sprites.Length; i++)
        {
            float elapsedTime = Time.time - startTime;

            while (elapsedTime < frameRate * (i + 1) && elapsedTime < displayDuration)
            {
                yield return null;
                elapsedTime = Time.time - startTime;
            }

            spriteRenderer.sprite = sprites[i];
        }

        spriteRenderer.sprite = sprites[sprites.Length - 1];
        yield return new WaitForSeconds(displayDuration - (Time.time - startTime));
        gameObject.SetActive(false);
        ObjectPooler.Instance.ReturnObject(gameObject);
    }
}
