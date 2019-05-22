using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemys : MonoBehaviour
{
    // Start is called before the first frame update
    public float RangeMinX = -15;
    public float RangeMaxX = 15;
    public Vector3 SpawnDistFromPlayer;

    public int AmountofEnemys = 0;
    public int RespawnTime = 5;
    public GameObject[] Resp;
    public GameObject Enemy;

    
    void Start()
    {
        Resp = new GameObject[AmountofEnemys];
        for (int i = 0; i < AmountofEnemys; i++)
        {
            SpawnDistFromPlayer.x = UnityEngine.Random.Range(RangeMinX, RangeMaxX);
            SpawnDistFromPlayer.y = UnityEngine.Random.Range(-5, 5);
            GameObject go = Instantiate(Enemy, SpawnDistFromPlayer, Quaternion.identity) as GameObject;
            go.transform.localScale = Vector3.one;
            Resp[i] = go;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int i = 0;
        while(i < AmountofEnemys)
        {
            Respawn(i);
            i++;
        }
        
    }
    IEnumerator Wait(int RespawnTime, int i)
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        Respawn(i);
    }
    public void Respawn(int i)
    {
        if(Resp[i].activeSelf == false)
        {
            SpawnDistFromPlayer.x = UnityEngine.Random.Range(RangeMinX, RangeMaxX);
            SpawnDistFromPlayer.y = UnityEngine.Random.Range(-5, 5);
            Resp[i].transform.position = SpawnDistFromPlayer;
            Resp[i].SetActive(true);
        }
        
        
    }

}
