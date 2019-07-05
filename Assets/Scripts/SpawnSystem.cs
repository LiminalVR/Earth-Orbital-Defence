using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// SpawnSystem spawns objects from a weighted list in a ring around the target. The 
/// </summary>
public class SpawnSystem 
    : MonoBehaviour
{
    public GameTimer Timer;
    public List<WeightedEnemy> HostileObjects;
    public AnimationCurve WaveSpawnCount;
    public AnimationCurve TimeBetweenWaves;
    public Transform EarthTransform;
    public float SpawnDistance;
    public bool Active;

    private float _timeSinceLastSpawn;
    private Coroutine SpawnerRoutine;

    public void StartSpawning()
    {
        _timeSinceLastSpawn = (int) TimeBetweenWaves.Evaluate(Timer.GetTime());
        Active = true;
    }

    private void Update()
    {
        if(!Active)
            return;

        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn < (int)TimeBetweenWaves.Evaluate(Timer.GetTime()))
            return;

        if (SpawnerRoutine != null)
            StopCoroutine(SpawnerRoutine);

        _timeSinceLastSpawn = 0;
        SpawnerRoutine = StartCoroutine(SpawnerCoro());
    }

    private IEnumerator SpawnerCoro()
    {
        var spawnedEnemies = 0;

        while (spawnedEnemies < (int)WaveSpawnCount.Evaluate(Timer.GetTime()))
        {
            var pos = RandomCircle(EarthTransform.position, SpawnDistance);

            var prefab = GetRandomWeightedPrefab(HostileObjects);
            Instantiate(prefab, pos, Quaternion.identity);

            spawnedEnemies++;

            yield return new WaitForEndOfFrame();
        }

        SpawnerRoutine = null;
    }

    public GameObject GetRandomWeightedPrefab(List<WeightedEnemy> weightedEnemies)
    {
        var weightSum = 0;

        foreach (var enemy in weightedEnemies)
        {
            weightSum += enemy.Weight;
        }

        var index = 0;
        var lastIndex = weightedEnemies.Count - 1;

        while (index < lastIndex)
        {
            if (Random.Range(0, weightSum) < weightedEnemies[index].Weight)
            {
                return weightedEnemies[index].Prefab;
            }

            weightSum -= weightedEnemies[index++].Weight;
        }

        return weightedEnemies[index].Prefab;
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        var ang = Random.value * 360;
        Vector3 pos;

        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }
}
