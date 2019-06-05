using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotation Class
public class Rotation : MonoBehaviour
{
    // Float Range
    [Range(5.0f, 20.0f)]
    public float rotationSpeed;

    // Update Rotation after Time
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * (rotationSpeed - 2f) * Time.deltaTime);
        transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
    }
}
