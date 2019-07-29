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
    public Text percetengeBox;
    private SphereCollider earthCollider;
    public GameObject explossionPrefab;
    public GameObject fog;
    public int earthHealth;
    public int currentHealth;
    public float mHealth;
    public float mPercentage;
    public AppController AppController;
    private bool dead = false;
    private GameObject geo;

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
        geo = GameObject.Instantiate(explossionPrefab, new Vector3 (0,0,0), new Quaternion ());
        geo.SetActive(false);
   

        currentHealth = earthHealth;
    }

    // BOOM!
    private void Update()
    {

        if (currentHealth <= 0.0f)
        {
            ParticleSystem mGeo = geo.GetComponent<ParticleSystem>();
            geo.transform.position = this.transform.position;
            geo.SetActive(true);
            mGeo.Play();

            this.currentHealth = earthHealth;
            mPercentage = 0;
            dead = true;
            this.gameObject.SetActive(false);
        }

        if (dead)
        {
            mPercentage = currentHealth;
            screenBar.fillAmount = mPercentage;
            percetengeBox.text = mPercentage.ToString("0");
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
        percetengeBox.text = mPercentage.ToString();
    }
}
