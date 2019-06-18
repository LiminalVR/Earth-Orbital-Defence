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
    public bool astroRotate = false;
    public bool objectRotate = false;

    // Update Rotation after Time
    private void FixedUpdate()
    {
        if (staticRotate)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (astroRotate)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
        }

        if(objectRotate)
        {
            transform.Rotate(Vector3.up * (Random.Range(50.0f * Time.deltaTime, 100.0f * Time.deltaTime)));
            transform.Rotate(Vector3.left * (Random.Range(50.0f * Time.deltaTime, 100.0f * Time.deltaTime)));
        }
    }
}
