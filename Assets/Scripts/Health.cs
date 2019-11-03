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
    public MeshRenderer ShieldRenderer;
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
    private static readonly int ShieldVisibility = Shader.PropertyToID("_ShieldVisibility");

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

        if (NormalisedHealth <= 0f && ShieldRenderer.gameObject.activeSelf == false)
        {
            StartCoroutine(ShieldActivation());
        }
    }

    private IEnumerator ShieldActivation()
    {
        var shieldVis = 0f;
        ShieldRenderer.transform.localScale = Vector3.zero;
        ShieldRenderer.material.SetFloat(ShieldVisibility, shieldVis);
        ShieldRenderer.gameObject.SetActive(true);

        while (shieldVis<1f)
        {
            ShieldRenderer.transform.localScale = Vector3.Lerp(Vector3.zero, ShieldFinalSize, shieldVis);
            shieldVis += Time.deltaTime * ShieldShowSpeed;
            ShieldRenderer.material.SetFloat(ShieldVisibility, shieldVis);
            yield return new WaitForEndOfFrame();
        }

        ShieldRenderer.material.SetFloat(ShieldVisibility, 1f);
    }

    public void Damage(int damageToTake)
    {
        currentHealth -= damageToTake;
        currentHealth = Mathf.Clamp(currentHealth, 0, earthHealth);
        mHealth = (float)currentHealth / (float)earthHealth;
        mPercentage = mHealth * earthHealth;
    }
}
