using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGem : MonoBehaviour
{
    private GemType gemType;
    private int indexNumber;

    private void Start()
    {
        gemType = GetComponent<Gem>().GetGemType();
        indexNumber = GetComponent<Gem>().GetIndexNumber();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Snap>() != null)
        {
            GameManager.Instance.EnableTheClosestGrid(gameObject.transform, indexNumber); 
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Snap>() != null)
        {
            GameManager.Instance.EnableTheClosestGrid(gameObject.transform, indexNumber);
            Destroy(gameObject);
        }
    }
}
