using System;
using UnityEngine;

[Serializable]
public class SpawnObject
{
    public GameObject prefab;
    [Range(0, 1f)] public float chance;
    [Range(-5f, 0)] public float minVertVariation;
    [Range(0, 5f)] public float maxVertVariation;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] public SpawnObject[] spawnObjects;

    [Header("Configuration")]
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;

    [Header("Debug")]
    [SerializeField] private Vector3 rightEdge;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float spawnAcceleration;

    private void Start()
    {
        if (spawnObjects.Length > 0)
            Invoke(nameof(Spawn), UnityEngine.Random.Range(minSpawnRate, maxSpawnRate));

        FixSpawnLocation();
    }

    private void FixSpawnLocation()
    {
        rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2));
        spawnPosition = new Vector3(rightEdge.x + 2, rightEdge.y);
        transform.position = spawnPosition;
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

        // Decreases the time between spawns the game speed increases (limited to 0,5s).
        if (spawnAcceleration < 0.5)
            spawnAcceleration = GameManager.Instance.gameSpeed / 100;

        Invoke(nameof(Spawn), UnityEngine.Random.Range(minSpawnRate - spawnAcceleration, maxSpawnRate - spawnAcceleration));
    }
}
