using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Health 
    : MonoBehaviour
    , IDamagable
{

    public MeshRenderer EarthMeshRenderer;
    public GameObject fog;
    public float ShieldShowSpeed;
    public Vector3 ShieldFinalSize;
    public int earthHealth;
    public int currentHealth;
    public float mHealth;
    public float mPercentage;
    public AppController AppController;
    public float NormalisedHealth
        => (float)currentHealth / earthHealth;

    private bool dead = false;
    private SphereCollider earthCollider;
    private Material _earthMat;
    private static readonly int HealthRemaining = Shader.PropertyToID("_HealthRemaining");

    private void OnValidate()
    {
        Assert.IsNotNull(AppController, "AppController must not be null");
    }

    // Initalizing Variables
    void Start()
    {
        earthCollider = GetComponent<SphereCollider>();
        earthCollider.isTrigger = true;
        mHealth = 0.0f;
        currentHealth = earthHealth;
        _earthMat = EarthMeshRenderer.material;
    }

    // BOOM!
    private void Update()
    {
        if (currentHealth <= 0.0f)
        {
            mPercentage = 0;
            dead = true;
        }

        if (_earthMat == null)
            return;

        _earthMat.SetFloat(HealthRemaining, NormalisedHealth);
    }

    public void Damage(int damageToTake, GameObject origin = null)
    {
        currentHealth -= damageToTake;
        currentHealth = Mathf.Clamp(currentHealth, 0, earthHealth);
        mHealth = (float)currentHealth / (float)earthHealth;
        mPercentage = mHealth * earthHealth;
    }
}
