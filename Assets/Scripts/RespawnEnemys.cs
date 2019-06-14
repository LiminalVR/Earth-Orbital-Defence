using System;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject health;
    public Button button1;
    public Button button2;

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
    private bool start = false;
    private bool finish = false;
    int i = 0;
    int u = 0;
    int v = 0;
    void Start()
    {
        DebriArr = new GameObject[MAXNUMOFDEBRI];
        RockArr = new GameObject[MAXNUMOFDEBRI];
        AlienArr = new GameObject[MAXNUMOFGREENBOI];
        button1.onClick.AddListener(StartG);
        button2.onClick.AddListener(StartG);
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


        if (start == true && finish == false)
        {
            if (Timer.getTime() <= StartingTime && Timer.getTime() >= 0)
            {
                timerDebri += Time.deltaTime;
                timerRock += Time.deltaTime;
                timerAlien += Time.deltaTime;
                spawnDebri();
                spawnRock();
                spawnAlien();
                if (timerDebri >= timeToWaitTillSpawnIncreaseDebri)
                {
                    if ((lastDebriSpawned + AmountDebriAddedAfterTime) < MAXNUMOFDEBRI - 1)
                    {
                        lastDebriSpawned += AmountDebriAddedAfterTime;
                        timerDebri = 0;
                    }
                    else if ((lastDebriSpawned + AmountDebriAddedAfterTime) > MAXNUMOFDEBRI - 1)
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
            if (!health.gameObject.activeInHierarchy)
            {
                

                finish = true;

            }
            if (Timer.getTime() <= 0)
            {
                

                finish = true;

            }
        }
        if (start == true && finish == true)
        {
            button2.gameObject.SetActive(true);
            i = 0;
            v = 0;
            u = 0;
            KillAlien();
            KillDebri();
            KillRock();
            lastAlienSpawned = 0;
            lastDebriSpawned = 0;
            lastRockSpawned = 0;
            
            start = false;
            finish = false;
        }


    }
    void spawnDebri()
    {
        if (i >= MAXNUMOFDEBRI +1)
        {
            i = 0;
        }
        if (!DebriArr[i].activeInHierarchy)
        {
            DebriArr[i].SetActive(true);
            Vector2 Spawn = UnityEngine.Random.insideUnitCircle.normalized;
            SpawnDistFromPlayer.x = Spawn.x * 1200;
            SpawnDistFromPlayer.z = Spawn.y * 1200 - (376);
            DebriArr[i].transform.position = SpawnDistFromPlayer;
            Debug.Log(i);

        }
        if (i <=lastDebriSpawned)
        {
            i++;
        }
        else
        {
            i = 0;
        }

    }

    void KillDebri()
    {

        for (int i = 0; i < MAXNUMOFDEBRI; i++)
        {
            if (DebriArr[i].activeInHierarchy)
            {
                DebriArr[i].SetActive(false);
            }
        }
    }
    void spawnRock()
    {
        if (u >= MAXNUMOFROCK)
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

    void KillRock()
    {

        for (int i = 0; i < MAXNUMOFROCK; i++)
        {
          
            if (RockArr[i].activeInHierarchy)
            {
                RockArr[i].SetActive(false);


            }
        }
    }
    void spawnAlien()
    {
        if (v >= MAXNUMOFGREENBOI)
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
    void KillAlien()
    {
        for (int i = 0; i < MAXNUMOFGREENBOI; i++)
        {
            if (AlienArr[i].activeInHierarchy)
            {
                AlienArr[i].SetActive(false);
            }
        }

    }
    void StartG()
    {
        if (start == false && button1.IsActive())
        {
            start = true;
            button1.gameObject.SetActive(false);
            return;
        }
        else if (start == false && !button1.IsActive())
        {
            start = true;
            button2.gameObject.SetActive(false);
            health.gameObject.SetActive(true);
            
            Timer.time = Timer.GetStart();
            return;
        }

    }



}
