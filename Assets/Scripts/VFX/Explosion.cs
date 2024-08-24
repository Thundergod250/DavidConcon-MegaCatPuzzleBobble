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
        float totalAnimationTime = frameRate * sprites.Length;
        float finalSpriteTime = Mathf.Max(0, displayDuration - totalAnimationTime);
        foreach (Sprite sprite in sprites)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(frameRate);
        }
        spriteRenderer.sprite = sprites[sprites.Length - 1];
        yield return new WaitForSeconds(finalSpriteTime);
        gameObject.SetActive(false);
    }
}
