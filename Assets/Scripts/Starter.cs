using UnityEngine;

/// <summary>
/// Starter is used to turn on a given gameobject when the system first starts. This is done to preserve any timing sensitive events within the scene that may be impacted by the loading of the limapp.
/// </summary>
public class Starter 
    : MonoBehaviour
{
    public GameObject SceneObject;

    void Start()
    {
        SceneObject.SetActive(true);
    }
}
