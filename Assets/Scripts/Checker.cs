using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    private Gem gem;

    private void Start()
    {
        gem = GetComponentInParent<Gem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Gem otherGem))
        {
            if (otherGem.GetGemType() == gem.GetGemType() && otherGem != gem)
            {
                gem.AddToGroupedGems(otherGem.gameObject);
                otherGem.AddToGroupedGems(gem.gameObject); 
                gem.SynchronizeGroupedGems();
                otherGem.SynchronizeGroupedGems();
                gem.DeactivateMatchedGems();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Gem otherGem))
        {
            if (otherGem != gem && gem.groupedGems.Contains(otherGem.gameObject))
            {
                gem.groupedGems.Remove(otherGem.gameObject);
            }
        }
    }
}
