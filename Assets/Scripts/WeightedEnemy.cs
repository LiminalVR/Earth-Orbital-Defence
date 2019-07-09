using System;

using UnityEngine;

/// <summary>
/// WeightedEnemy is used to store the enemy prefab to be spawned by <see cref="SpawnSystem"/> as well as how heavily the system should weight the entry when choosing which prefab to spawn.
/// </summary>
[Serializable]
public class WeightedEnemy
{
    public Enemy Prefab;
    public int Weight;
}
