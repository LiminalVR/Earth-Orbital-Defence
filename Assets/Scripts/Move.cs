using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int Speedmin = 0;
    public float Speedmax = 20;

    int speed;
    void Start()
    {
        //speed = UnityEngine.Random.Range(Speedmin, Speedmax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward*0.001f, Space.Self);
    }
}
