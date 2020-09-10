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
    public AppController Timer;
    public List<WeightedEnemy> HostileObjects;
    public AnimationCurve WaveSpawnCount;
    public AnimationCurve TimeBetweenWaves;
    [Space]
    public List<Enemy> ActiveEnemies = new List<Enemy>();
    [Space]
    public Transform EarthTransform;
    public float SpawnDistance;
    public bool Active;

    private float _timeSinceLastSpawn;
    private Coroutine SpawnerRoutine;

    public void StartSpawning()
    {
        _timeSinceLastSpawn = (int)TimeBetweenWaves.Evaluate(Timer.CurrentTime);
        Active = true;
    }

    private void Update()
    {
        if(!Active)
            return;

        if (ActiveEnemies.Count > 2)
            return;

        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn < (int)TimeBetweenWaves.Evaluate(Timer.CurrentTime))
            return;

        if (SpawnerRoutine != null)
            StopCoroutine(SpawnerRoutine);

        _timeSinceLastSpawn = 0;
        SpawnerRoutine = StartCoroutine(SpawnerCoro());
    }

    private IEnumerator SpawnerCoro()
    {
        var spawnedEnemies = 0;
        var enemyCount = (int)WaveSpawnCount.Evaluate(Timer.CurrentTime);

        for (var i = 0; i < enemyCount; i++)
        {
            var pos = GetPosInCircle(EarthTransform.position, Random.Range(SpawnDistance, SpawnDistance * 1.1f), 360f/enemyCount, i);
            var prefab = GetRandomWeightedPrefab(HostileObjects);
            var enemy = Instantiate(prefab, pos, Quaternion.identity);
            enemy.SpawnSystem = this;
            enemy.earth = EarthTransform.gameObject;

            ActiveEnemies.Add(enemy);

            spawnedEnemies++;

            yield return new WaitForEndOfFrame();
        }

        SpawnerRoutine = null;
    }

    private Enemy GetRandomWeightedPrefab(List<WeightedEnemy> weightedEnemies)
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

    private Vector3 GetPosInCircle(Vector3 center, float radius, float angle, int itemNumber)
    {
        var rotation = Quaternion.AngleAxis(itemNumber * angle, transform.up);
        var direction = rotation * transform.forward;
        var position = center + (direction * radius);

        return position;
    }
}
