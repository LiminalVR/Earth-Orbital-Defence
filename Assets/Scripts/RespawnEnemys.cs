using System;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemys : MonoBehaviour
{
    // Start is called before the first frame update


    public Vector3 SpawnDistFromPlayer;

    // public int AmountofEnemys = 0;

    public WaveTimer Timer;
    public GameObject[] Resp;
    public GameObject Debri;
    public GameObject Rock;
    public GameObject Boi;

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


    void Start()
    {

        max = MAXNUMOFDEBRI + MAXNUMOFROCK + MAXNUMOFGREENBOI;
        Resp = new GameObject[max];
        for (int i = 0; i < MAXNUMOFDEBRI; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
            GameObject go = Instantiate(Debri, SpawnDistFromPlayer, Quaternion.identity) as GameObject;

            Resp[i] = go;
            Resp[i].SetActive(false);
            lastDebriSpawned = i;
        }
        for (int i = MAXNUMOFDEBRI; i < max; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
            GameObject go = Instantiate(Rock, SpawnDistFromPlayer, Quaternion.identity) as GameObject;

            Resp[i] = go;
            Resp[i].SetActive(false);
            lastRockSpawned = i;
        }
        for (int i = MAXNUMOFROCK + MAXNUMOFDEBRI; i < max; i++)
        {
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 2400;
            SpawnDistFromPlayer.z = Spawn.y * 2400 - (376);
            GameObject go = Instantiate(Boi, SpawnDistFromPlayer, Quaternion.identity) as GameObject;

            Resp[i] = go;
            Resp[i].SetActive(false);
            lastAlienSpawned = i;
        }
    }

    // Update is called once per frame
    void Update()
    {

        timerDebri += Time.deltaTime;
        timerRock += Time.deltaTime;
        timerAlien += Time.deltaTime;
        if (Timer.getTime() <= StartingTime)
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
            for (int i = MAXNUMOFDEBRI; i < MAXNUMOFDEBRI + startingAmtRock; i++)
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
            for (int i = MAXNUMOFDEBRI + MAXNUMOFROCK; i < MAXNUMOFDEBRI + MAXNUMOFROCK + startingAmtBoi; i++)
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
        if (timerDebri >= timeToWaitTillSpawnIncreaseDebri)
        {
            int size = lastDebriSpawned + AmountDebriAddedAfterTime;

            if (size > MAXNUMOFDEBRI)
                size = MAXNUMOFDEBRI - 1;
            for (int i = lastDebriSpawned; i < size; i++)
            {
                if (!Resp[i].activeInHierarchy)
                {
                    Resp[i].SetActive(true);
                    Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
                    SpawnDistFromPlayer.x = Spawn.x * 1200;
                    SpawnDistFromPlayer.z = Spawn.y * 1200 - (376);
                    Resp[i].transform.position = SpawnDistFromPlayer;
                    lastDebriSpawned = i;
                }
            }
            timerDebri = 0;

        }
        if (timerRock >= timeToWaitTillSpawnIncreaseRock)
        {
            int size = lastRockSpawned + AmountRockAddedAfterTime;

            if (size > MAXNUMOFROCK)
                size = MAXNUMOFROCK - 1;
            for (int i = lastRockSpawned; i < size; i++)
            {
                if (!Resp[i].activeInHierarchy)
                {
                    Resp[i].SetActive(true);
                    Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
                    SpawnDistFromPlayer.x = Spawn.x * 1200;
                    SpawnDistFromPlayer.z = Spawn.y * 1200 - (376);
                    Resp[i].transform.position = SpawnDistFromPlayer;
                    lastRockSpawned = i;
                }
            }
            timerAlien = 0;

        }
        if (timerAlien >= timeToWaitTillSpawnIncreaseAlien)
        {
            int size = lastAlienSpawned + AmountAlienAddedAfterTime;

            if (size > MAXNUMOFGREENBOI)
                size = MAXNUMOFGREENBOI - 1;
            for (int i = lastAlienSpawned; i < size; i++)
            {
                if (!Resp[i].activeInHierarchy)
                {
                    Resp[i].SetActive(true);
                    Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
                    SpawnDistFromPlayer.x = Spawn.x * 1200;
                    SpawnDistFromPlayer.z = Spawn.y * 1200 - (376);
                    Resp[i].transform.position = SpawnDistFromPlayer;
                    lastAlienSpawned = i;
                }
            }
            timerAlien = 0;

        }

    }


}
