using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Health 
    : MonoBehaviour
    , IDamagable
{
    public Image earthBar;
    public Image screenBar;
    private SphereCollider earthCollider;
    public GameObject fog;
    public int earthHealth;
    public int currentHealth;
    public float mHealth;
    public float mPercentage;
    public AppController AppController;
    public float NormalisedHealth
        => (float)currentHealth / earthHealth;
    private bool dead = false;

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
    }

    // BOOM!
    private void Update()
    {
        if (currentHealth <= 0.0f)
        {
            mPercentage = 0;
            dead = true;
        }

        return;
        if (dead)
        {
            mPercentage = currentHealth;
            screenBar.fillAmount = mPercentage;
            dead = false;

            AppController.SetIsEnded(true);
        }
    }

    public void Damage(int damageToTake)
    {
        currentHealth -= damageToTake;
        currentHealth = Mathf.Clamp(currentHealth, 0, earthHealth);
        mHealth = (float)currentHealth / (float)earthHealth;
        earthBar.fillAmount = mHealth;
        screenBar.fillAmount = mHealth;
        mPercentage = mHealth * earthHealth;
    }
}
