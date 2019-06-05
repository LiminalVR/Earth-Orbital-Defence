using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int Speedmin = 4;
    public float Speedmax = 20;
    public GameObject earth;
<<<<<<< HEAD
    public GameObject Projectile;
    float timer;
    public float Basespeed;
=======
    public float Basespeed = 1f;
>>>>>>> Ivan-Aupart
    
    int speed;
    void Start()
    {
        speed = ((int)Basespeed * (int)UnityEngine.Random.Range(Speedmin, Speedmax)); 
    }
    void Update()
    {
        if(gameObject.activeSelf == true)
        {
<<<<<<< HEAD

            float step = speed * Time.deltaTime;
            if(Vector3.Distance(transform.position, earth.transform.position) < 100f && gameObject.tag == "Alien")
            {
                timer = 2* Time.deltaTime;
                Debug.Log("yes");
                if(timer >= 4)
                {
                    Fire();
                    timer = 0;
                }
               
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, step);


                if (Vector3.Distance(transform.position, earth.transform.position) < 0.001f)
                {

                    gameObject.SetActive(false);
                }
            }
            
=======
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, step);


            if (Vector3.Distance(transform.position, earth.transform.position) < 0.001f)
            {

                gameObject.SetActive(false);
            }
>>>>>>> Ivan-Aupart
        }
        
       
        
            
        
    }
<<<<<<< HEAD
    void Fire()
    {
        GameObject go = Instantiate(Projectile, gameObject.transform.position, Quaternion.identity) as GameObject;
        float step = 2 * Time.deltaTime;
        go.transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, step);
    }
=======
>>>>>>> Ivan-Aupart
}
