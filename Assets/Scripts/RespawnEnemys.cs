using System;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemys : MonoBehaviour
{
    // Start is called before the first frame update
    public float RangeMinX = -15;
    public float RangeMaxX = 15;
    public Vector3 SpawnDistFromPlayer;

   // public int AmountofEnemys = 0;
    public int RespawnTime = 5;
    public GameObject[] Resp;
    public GameObject Debri;
    public GameObject Rock;
    public GameObject Boi;
    public int MAXNUMOFDEBRI = 0;
    public int MAXNUMOFROCK = 0;
    public int MAXNUMOFGREENBOI = 0;
    int max;
    public int StartingTime;
    public int startingAmtDebri;
    public int startingAmtRock;
    public int startingAmtBoi;
    float timer = 0.0f;


    void Start()
    {
        max = MAXNUMOFDEBRI + MAXNUMOFROCK + MAXNUMOFGREENBOI;
        Resp = new GameObject[max];
        for (int i = 0; i < MAXNUMOFDEBRI; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x =  Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - ( 376); 
            GameObject go = Instantiate(Debri, SpawnDistFromPlayer, Quaternion.identity) as GameObject;
            
            Resp[i] = go;
            Resp[i].SetActive(false);
        }
        for (int i = MAXNUMOFDEBRI; i < max; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
            GameObject go = Instantiate(Rock, SpawnDistFromPlayer, Quaternion.identity) as GameObject;

            Resp[i] = go;
            Resp[i].SetActive(false);
        }
        for (int i = MAXNUMOFROCK + MAXNUMOFDEBRI; i < max ; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
            GameObject go = Instantiate(Boi, SpawnDistFromPlayer, Quaternion.identity) as GameObject;

            Resp[i] = go;
            Resp[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if(timer >= StartingTime)
        {
            for (int i = 0; i < startingAmtDebri; i++)
            {
                if (!Resp[i].activeInHierarchy)
                {
                    Resp[i].SetActive(true);
                    Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
                    SpawnDistFromPlayer.x = Spawn.x * 1200;
                    SpawnDistFromPlayer.z = Spawn.y * 1200 - (376);
                    Resp[i].transform.position = SpawnDistFromPlayer;

                }
            }
            for (int i = MAXNUMOFDEBRI; i < MAXNUMOFDEBRI+startingAmtRock; i++)
            {
                if (!Resp[i].activeInHierarchy)
                {
                    Resp[i].SetActive(true);
                    Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
                    SpawnDistFromPlayer.x = Spawn.x * 2400;
                    SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
                    Resp[i].transform.position = SpawnDistFromPlayer;

                }
            }
            for (int i = MAXNUMOFDEBRI+ MAXNUMOFROCK; i < MAXNUMOFDEBRI+MAXNUMOFROCK+startingAmtBoi; i++)
            {
                if (!Resp[i].activeInHierarchy)
                {
                    Resp[i].SetActive(true);
                    Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
                    SpawnDistFromPlayer.x = Spawn.x * 2400;
                    SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
                    Resp[i].transform.position = SpawnDistFromPlayer;

                }
            }
        }
        
    }
    IEnumerator Wait()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        Respawn();
    }
    public void Respawn()
    {
        
        
        
        
    }

}
