using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOff : MonoBehaviour
{
    [SerializeField] private Gem gem;

    private void OnEnable()
    {
        StartCoroutine(DisableAfterDelay(0.8f));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.TryGetComponent(out Gem otherGem) && otherGem != gem) || other.GetComponent<Snap>() != null)
        {

        }
        else
        {
            //gem.gameObject.SetActive(false);
        }
        /*if (other.TryGetComponent(out Gem otherGem) && otherGem != gem)
        {
            otherGem.EnableFallOff();
            Debug.LogWarning("TESTOR");
        }
        else if (other.GetComponent<Snap>() != null)
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.FallTimerToClear());
            Debug.LogWarning("TESTOR2");
        }
        else
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.FallTimerToFall());
            Debug.LogWarning("TESTOR3");
        }*/
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
