using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    [SerializeField] private float fallInterval = 5.0f; 
    [SerializeField] private float fallAmount = 0.1f;   

    private Coroutine fallCoroutine;

    private void Start()
    {
        fallCoroutine = StartCoroutine(FallDownRoutine());
    }

    private IEnumerator FallDownRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fallInterval);
            transform.position += new Vector3(0, -fallAmount, 0);
        }
    }

    public void StopFalling()
    {
        if (fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
            fallCoroutine = null;
        }
    }

    public void StartFalling()
    {
        if (fallCoroutine == null)
        {
            fallCoroutine = StartCoroutine(FallDownRoutine());
        }
    }
}
