using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    private SphereCollider mSphereCollider;

    public GameObject explossionPrefab;

    public int earthHealth;
    private int currentHealth;
    public float mHealth;
 
    private int damageOne;
    private int damageTwo;
    private int damageThree;

    // Initalizing Variables
    void Start()
    {
        mSphereCollider = GetComponent<SphereCollider>();
        mSphereCollider.isTrigger = true;
        mHealth = 1.0f;

        currentHealth = earthHealth;

        damageOne = 1;
        damageTwo = 2;
        damageThree = 3;
    }

    // BOOM!
    private void Update()
    {
        if(currentHealth <= 0.0f)
        {
            Instantiate(explossionPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    // Updates Health Bar on Trigger Enter 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= damageOne;
            mHealth = (float)currentHealth / (float)earthHealth;
            healthBar.fillAmount = mHealth;
            print("Enemy1 has collided!!");
            print(mHealth);
        }

        if (other.gameObject.CompareTag("Enemy2"))
        {
            currentHealth -= damageTwo;
            mHealth = (float)currentHealth / (float)earthHealth;
            healthBar.fillAmount = mHealth;
            print("Enemy2 has collided!!");
        }

        if (other.gameObject.CompareTag("Enemy3"))
        {
            currentHealth -= damageThree;
            mHealth = (float)currentHealth / (float)earthHealth;
            healthBar.fillAmount = mHealth;
            print("Enemy3 has collided!!");
        }
    }
}
