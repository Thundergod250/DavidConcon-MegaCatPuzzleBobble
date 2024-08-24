using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float minRotation = -70f; 
    [SerializeField] private float maxRotation = 80f; 
    [SerializeField] private float currentRotation = 0f; 

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
        {
            currentRotation += -horizontalInput * rotationSpeed * Time.deltaTime;
            currentRotation = Mathf.Clamp(currentRotation, minRotation, maxRotation);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        }
    }
}
