using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int Speedmin = 4;
    public float Speedmax = 20;
    public GameObject earth;
    public float Basespeed;
    
    int speed;
    void Start()
    {
        speed = ((int)Basespeed * (int)UnityEngine.Random.Range(Speedmin, Speedmax)); 
    }
    void Update()
    {
        if(gameObject.activeSelf == true)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, step);


            if (Vector3.Distance(transform.position, earth.transform.position) < 0.001f)
            {

                gameObject.SetActive(false);
            }
        }
        
       
        
            
        
    }
}
