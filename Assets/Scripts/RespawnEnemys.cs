using System;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemys : MonoBehaviour
{
    // Start is called before the first frame update


    public Vector3 SpawnDistFromPlayer;

    public WaveTimer Timer;
    private GameObject[] DebriArr;
    public GameObject Debri;
    private GameObject[] RockArr;
    public GameObject Rock;
    private GameObject[] AlienArr;
    public GameObject Alien;

    public int MAXNUMOFDEBRI = 0;
    public int MAXNUMOFROCK = 0;
    public int MAXNUMOFGREENBOI = 0;
    public int StartingTime;
    public int AmountDebriAddedAfterTime;
    public int AmountRockAddedAfterTime;
    public int AmountAlienAddedAfterTime;
    public int startingAmtDebri;
    public int startingAmtRock;
    public int startingAmtBoi;
    private int lastDebriSpawned;
    private int lastRockSpawned;
    private int lastAlienSpawned;
    private int max;
    public float timeToWaitTillSpawnIncreaseDebri;
    public float timeToWaitTillSpawnIncreaseRock;
    public float timeToWaitTillSpawnIncreaseAlien;
    private float timerDebri;
    private float timerRock;
    private float timerAlien;
    int i =0;
    int u = 0;
    int v = 0;
    void Start()
    {
        DebriArr = new GameObject[MAXNUMOFDEBRI];
        RockArr = new GameObject[MAXNUMOFDEBRI];
        AlienArr = new GameObject[MAXNUMOFGREENBOI];
        for (int i = 0; i < MAXNUMOFDEBRI; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
            GameObject go = Instantiate(Debri, SpawnDistFromPlayer, Quaternion.identity) as GameObject;
            DebriArr[i] = go;
            DebriArr[i].SetActive(false);

        }
        lastDebriSpawned = startingAmtDebri;
        for (int i = 0; i < MAXNUMOFROCK; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
            GameObject go = Instantiate(Rock, SpawnDistFromPlayer, Quaternion.identity) as GameObject;
            RockArr[i] = go;
            RockArr[i].SetActive(false);

        }
        lastRockSpawned = startingAmtRock;
        for (int i = 0; i < MAXNUMOFGREENBOI; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
            GameObject go = Instantiate(Alien, SpawnDistFromPlayer, Quaternion.identity) as GameObject;
            AlienArr[i] = go;
            AlienArr[i].SetActive(false);

        }
        lastAlienSpawned = startingAmtBoi;
    }

    // Update is called once per frame
    void Update()
    {

        
        
        if (Timer.getTime() <= StartingTime)
        {
            timerDebri += Time.deltaTime;
            timerRock += Time.deltaTime;
            timerAlien += Time.deltaTime;
            spawnDebri();
            spawnRock();
            spawnAlien();
            if(timerDebri >= timeToWaitTillSpawnIncreaseDebri)
            {
                if((lastDebriSpawned + AmountDebriAddedAfterTime) < MAXNUMOFDEBRI -1)
                {
                    lastDebriSpawned += AmountDebriAddedAfterTime;
                    timerDebri = 0;
                }
                else if((lastDebriSpawned + AmountDebriAddedAfterTime) > MAXNUMOFDEBRI - 1 )
                {
                    lastDebriSpawned = MAXNUMOFDEBRI - 1;
                    timerDebri = 0;
                }
                
            }
            if (timerRock >= timeToWaitTillSpawnIncreaseRock)
            {
                if ((lastRockSpawned + AmountRockAddedAfterTime) < MAXNUMOFROCK - 1)
                {
                    lastRockSpawned += AmountRockAddedAfterTime;
                    timerRock = 0;
                }
                else if ((lastRockSpawned + AmountRockAddedAfterTime) > MAXNUMOFROCK - 1)
                {
                    lastRockSpawned = MAXNUMOFROCK - 1;
                    timerRock = 0;
                }

            }
            if (timerAlien >= timeToWaitTillSpawnIncreaseAlien)
            {
                if ((lastAlienSpawned + AmountAlienAddedAfterTime) < MAXNUMOFGREENBOI - 1)
                {
                    lastAlienSpawned += AmountAlienAddedAfterTime;
                    timerAlien = 0;
                }
                else if ((lastAlienSpawned + AmountAlienAddedAfterTime) > MAXNUMOFGREENBOI - 1)
                {
                    lastAlienSpawned = MAXNUMOFGREENBOI - 1;
                    timerAlien = 0;
                }

            }


        }

    }
    void spawnDebri()
    {
        if(i >=100)
        {
            i = 0;
        }
        if(!DebriArr[i].activeInHierarchy)
        {
            DebriArr[i].SetActive(true);
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 1200;
            SpawnDistFromPlayer.z = Spawn.y * 1200 - (376);
            DebriArr[i].transform.position = SpawnDistFromPlayer;
            Debug.Log(i);
            
        }
        if (i <= lastDebriSpawned)
        {
            i++;
        }
        else
        {
            i = 0;
        }

    }
    void spawnRock()
    {
        if (u >= 100)
        {
            u = 0;
        }
        if (!RockArr[u].activeInHierarchy)
        {
            RockArr[u].SetActive(true);
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 1200;
            SpawnDistFromPlayer.z = Spawn.y * 1200 - (376);
            RockArr[u].transform.position = SpawnDistFromPlayer;
            Debug.Log(u);

        }
        if (u <= lastRockSpawned)
        {
            u++;
        }
        else
        {
            u = 0;
        }

    }
    void spawnAlien()
    {
        if (v >= 100)
        {
            v = 0;
        }
        if (!AlienArr[v].activeInHierarchy)
        {
            AlienArr[v].SetActive(true);
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 1200;
            SpawnDistFromPlayer.z = Spawn.y * 1200 - (376);
            AlienArr[v].transform.position = SpawnDistFromPlayer;
            Debug.Log(v);

        }
        if (v <= lastAlienSpawned)
        {
            v++;
        }
        else
        {
            v = 0;
        }

    }

}
