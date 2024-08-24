using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private Gem gem;

    private void OnEnable()
    {
        GameManager.Instance.AddToDestroy(gem.gameObject);
        StartCoroutine(DisableAfterDelay(0.6f));
    }

    private void OnDisable()
    {
        StopAllCoroutines(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Gem otherGem))
        {
            if (otherGem.GetGemType() == gem.GetGemType() && otherGem != gem)
            {
                otherGem.EnableChecker();
            }
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DisableThisObject();
    }

    private void DisableThisObject()
    {
        gameObject.SetActive(false);
    }
}
