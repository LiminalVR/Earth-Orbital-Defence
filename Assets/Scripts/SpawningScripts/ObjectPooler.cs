using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [System.Serializable]
    public class Pool
    {
        public string objectTag;
        public GameObject prefab;
        public int numberOfObjects;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;


    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.numberOfObjects; i++)
            {
                GameObject mObject = Instantiate(pool.prefab);
                mObject.SetActive(false);
                objectPool.Enqueue(mObject);
            }

            poolDictionary.Add(pool.objectTag, objectPool);
        }
    }

    public GameObject SpwanFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " does not exist.");
            return null;
        }

       GameObject objectToSpawn = poolDictionary[tag].Dequeue();
       objectToSpawn.SetActive(true);
       objectToSpawn.transform.position = position;
       objectToSpawn.transform.rotation = rotation;

       poolDictionary[tag].Enqueue(objectToSpawn);
       return objectToSpawn;
    }

    public GameObject SpawnSpecialEffectsFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " does not exist.");
            return null;
        }

        GameObject specialEffectToSpawn = poolDictionary[tag].Dequeue();
        specialEffectToSpawn.SetActive(true);
        specialEffectToSpawn.transform.position = position;
        specialEffectToSpawn.transform.rotation = rotation;


        specialEffectToSpawn.SetActive(false);
        poolDictionary[tag].Enqueue(specialEffectToSpawn);
        return specialEffectToSpawn;
    }
}
