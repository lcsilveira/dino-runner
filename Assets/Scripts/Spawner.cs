using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnObject
{
    public GameObject prefab;
    [Range(0, 1f)] public float chance;
    [Range(-1f, 0)] public float minVertVariation;
    [Range(0, 1f)] public float maxVertVariation;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] public SpawnObject[] spawnObjects;

    [Header("Configuration")]
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;

    private void Start()
    {
        if (spawnObjects.Length > 0)
            Invoke(nameof(Spawn), UnityEngine.Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void Spawn()
    {
        float chanceSelector = UnityEngine.Random.Range(0f, 1f);
        foreach (var spawnObject in spawnObjects)
        {
            if (spawnObject.prefab != null && chanceSelector <= spawnObject.chance)
            {
                GameObject newObj = Instantiate(spawnObject.prefab, transform);

                float moveVertically = UnityEngine.Random.Range(spawnObject.minVertVariation, spawnObject.maxVertVariation);
                if (moveVertically != 0)
                    newObj.transform.localPosition += Vector3.up * moveVertically;

                break;
            }
            chanceSelector -= spawnObject.chance;
        }

        Invoke(nameof(Spawn), UnityEngine.Random.Range(minSpawnRate, maxSpawnRate));
    }
}
