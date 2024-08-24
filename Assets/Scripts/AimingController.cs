using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float minRotation = -70f; // Minimum rotation angle
    public float maxRotation = 80f; // Maximum rotation angle

    private float currentRotation = 0f; // Store the current rotation angle

    void Update()
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
