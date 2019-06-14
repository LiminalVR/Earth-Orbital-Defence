using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image earthBar;
    public Image screenBar;
    private SphereCollider earthCollider;

    public GameObject explossionPrefab;
    GameObject geo;

    public int earthHealth;
    public int currentHealth;
    public float mHealth;

    private int damageOne;
    private int damageTwo;
    private int damageThree;

    // Initalizing Variables
    void Start()
    {
        earthCollider = GetComponent<SphereCollider>();
        earthCollider.isTrigger = true;
        mHealth = 1.0f;
        geo = GameObject.Instantiate(explossionPrefab, new Vector3 (0,0,0), new Quaternion ());
        geo.SetActive(false);
   

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
            geo.transform.position = this.transform.position;
            geo.SetActive(true);

            this.currentHealth = earthHealth;
            this.gameObject.SetActive(false);

        }
    }

    // Updates Health Bar on Trigger Enter 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= damageOne;
            mHealth = (float)currentHealth / (float)earthHealth;
            earthBar.fillAmount = mHealth;
            screenBar.fillAmount = mHealth;
            print("Enemy1 has collided!!");
            print(mHealth);
            
        }
       
    }
}
