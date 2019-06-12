using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotation Class
public class Rotation : MonoBehaviour
{
    // Float Range
    [Range(3.0f, 200.0f)]
    public float rotationSpeed;
   public bool staticRotate = false;

    // Update Rotation after Time
    private void FixedUpdate()
    {
        if (staticRotate)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        else
        {
            transform.Rotate(Vector3.up * (rotationSpeed - 2f) * Time.deltaTime);
            transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
        }
    }
}
