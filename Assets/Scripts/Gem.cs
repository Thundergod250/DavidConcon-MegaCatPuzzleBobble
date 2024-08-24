using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GemType gemType;
    [SerializeField] private Sprite[] gemSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int indexNumber;
    [SerializeField] private Checker checker;
    [SerializeField] private FallOff fallOff;
    public List<GameObject> groupedGems = new();

    public GemType GetGemType() => gemType;
    public int GetIndexNumber() => indexNumber;

    public void RegisterFallOff()
    {
        GameManager.Instance.RegisterFallOff(fallOff);
    }

    public void RandomizeGemType()
    {
        int enumLength = System.Enum.GetValues(typeof(GemType)).Length;
        indexNumber = Random.Range(0, enumLength);
        SetType(indexNumber);
    }

    public void EnableChecker()
    {
        checker.gameObject.SetActive(true);
    }

    public void EnableFallOff()
    {
        fallOff.gameObject.SetActive(true);
    }

    public void SetType(int index)
    {
        gemType = (GemType)index;
        spriteRenderer.sprite = gemSprites[index];
    }

    public void Activate(int index)
    {
        SetType(index);
        checker.gameObject.SetActive(true);
        //fallOff.gameObject.SetActive(true);
    }
}
