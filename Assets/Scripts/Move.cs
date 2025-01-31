﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int Speedmin = 4;
    public float Speedmax = 20;
    public GameObject earth;
    public GameObject Projectile;
    public ParticleSystem collisionExplossion;
    float timer;
    public float Basespeed;
    public bool spaceship = false;

    int speed;
    void Start()
    {
        speed = ((int)Basespeed * (int)UnityEngine.Random.Range(Speedmin, Speedmax));
    }
    void Update()
    {
        if (gameObject.activeSelf == true)
        {

            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, step);
            Vector3 relativePos = earth.transform.position - transform.position;

            if (spaceship)
            {
                transform.LookAt(earth.transform.position);
            }


            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            if (Vector3.Distance(transform.position, earth.transform.position) < 0.001f)
            {

                gameObject.SetActive(false);
            }


        }
    }
    void Fire()
    {
        GameObject go = Instantiate(Projectile, gameObject.transform.position, Quaternion.identity) as GameObject;
        float step = 2 * Time.deltaTime;
        go.transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(collisionExplossion, this.transform.position, this.transform.rotation);
       // collisionExplossion.transform.position = this.gameObject.transform.position;
        collisionExplossion.Play();
        this.gameObject.SetActive(false);
    }
}
