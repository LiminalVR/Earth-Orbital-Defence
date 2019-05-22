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
            // SpawnDistFromPlayer.x = UnityEngine.Random.Range(RangeMinX, RangeMaxX);

            // SpawnDistFromPlayer.z = UnityEngine.Random.Range(-15, 100);

            SpawnDistFromPlayer.x = UnityEngine.Random.insideUnitCircle.x * 4000;
            SpawnDistFromPlayer.z = (UnityEngine.Random.insideUnitCircle.y * 4000 -( 376)); 
            GameObject go = Instantiate(Enemy, SpawnDistFromPlayer, Quaternion.identity) as GameObject;
           
            Resp[i] = go;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < AmountofEnemys; i++)
        {
            if (!Resp[i].activeInHierarchy)
            {
                Resp[i].SetActive(true);
                SpawnDistFromPlayer.x = UnityEngine.Random.insideUnitCircle.x * 4000;
                SpawnDistFromPlayer.z = (UnityEngine.Random.insideUnitCircle.y * 4000 - (376));
                Resp[i].transform.position = SpawnDistFromPlayer;

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
