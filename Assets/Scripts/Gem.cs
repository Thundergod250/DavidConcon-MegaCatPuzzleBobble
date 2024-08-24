using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GemType gemType;
    [SerializeField] private Sprite[] gemSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int indexNumber;
    [SerializeField] private GameObject checkerTrigger;
    public List<GameObject> groupedGems = new();

    public GemType GetGemType() => gemType;
    public int GetIndexNumber() => indexNumber;

    public void RandomizeGemType()
    {
        indexNumber = Random.Range(0, 6);
        SetType(indexNumber);
    }

    public void InitializeGems()
    {
        indexNumber = Random.Range(0, 6);
        SetType(indexNumber);
    }

    public void SetType(int index)
    {
        gemType = (GemType)index;
        spriteRenderer.sprite = gemSprites[index];
    }

    public void Activate(int index)
    {
        SetType(index);
        AddToGroupedGems(gameObject);
        checkerTrigger.SetActive(true);
        SynchronizeGroupedGems(); 
    }

    public void AddToGroupedGems(GameObject gemObject)
    {
        if (!groupedGems.Contains(gemObject))
        {
            groupedGems.Add(gemObject);
        }
    }

    public void ClearGroupedGems()
    {
        groupedGems.Clear();
    }

    public void DeactivateMatchedGems()
    {
        if (groupedGems.Count >= 3)
        {
            List<GameObject> gemsToDeactivate = new(groupedGems);

            foreach (GameObject gemObject in gemsToDeactivate)
            {
                gemObject.SetActive(false);
            }
            ClearGroupedGems();  
        }
    }

    public void SynchronizeGroupedGems()
    {
        List<GameObject> gemsToCheck = new(groupedGems);
        foreach (GameObject gemObject in gemsToCheck)
        {
            if (gemObject.TryGetComponent(out Gem gemScript))
            {
                foreach (GameObject adjacentGem in gemScript.groupedGems)
                {
                    if (!groupedGems.Contains(adjacentGem))
                    {
                        AddToGroupedGems(adjacentGem);
                    }
                }
            }
        }
    }
}
