using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs; // Assign enemy prefabs in Unity Inspector
    [SerializeField] private float spawnInterval = 2f; // Time between spawns

    private float leftEndLimit = -6f;
    private float rightEndLimit = 7f;

    [SerializeField] private bool shouldSpawnEnemies; // Set to true to start spawning
    [SerializeField] private float spawnTimer = 0f;

    private void Update()
    {
        if (shouldSpawnEnemies)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;//spawn timer
            }
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPrefabs.Count > 0)
        {
            Enemy randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            Renderer transformRenderer = GameObject.Find("Ground").GetComponent<Renderer>();
            float zEndDepth = transformRenderer.bounds.max.z;

            Vector3 spawnPosition = new Vector3(Random.Range(leftEndLimit, rightEndLimit), 1.15f, zEndDepth);

            // Debug.Log(spawnPosition);
            Instantiate(randomEnemy, spawnPosition, Quaternion.identity);
        }
    }
    public void StopSpawnEnemy()
    {
        shouldSpawnEnemies = false;
    }


    public void CheckIfHaveSpace()
    {

    }
}