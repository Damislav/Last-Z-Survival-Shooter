using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoostSpawner : MonoBehaviour
{
    [SerializeField] private Transform groundTransform;

    [SerializeField] Vector3 leftSide;
    [SerializeField] Vector3 rightSide;

    [SerializeField] private List<Boost> boosts;

    [SerializeField] private bool shouldSpawnBoost;

    [SerializeField] private float spawnInterval = 5f; // Time between spawns
    [SerializeField] private float spawnTimer = 0f;

    void Start()
    {
        groundTransform = GameObject.Find("Ground").transform;

        (leftSide, rightSide) = CalculateGroundWidthThenSplit();

    }

    private void Update()
    {
        if (shouldSpawnBoost)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnBoost();
                spawnTimer = 0f;//spawn timer
            }
        }
    }

    public void SpawnBoost()
    {

        (leftSide, rightSide) = CalculateGroundWidthThenSplit();

        if (boosts.Count == 0)
        {
            Debug.LogError("No boosts assigned!");
            return;
        }

        Instantiate(boosts[Random.Range(0, boosts.Count)], leftSide, Quaternion.identity);
        Instantiate(boosts[Random.Range(0, boosts.Count)], rightSide, Quaternion.identity);
    }

    public void StopSpawnBoost()
    {
        shouldSpawnBoost = false;
    }

    (Vector3, Vector3) CalculateGroundWidthThenSplit()
    {
        if (groundTransform == null)
        {
            Debug.LogError("Ground transform is not assigned!");
            return (Vector3.zero, Vector3.zero);
        }

        Renderer groundRenderer = groundTransform.GetComponent<Renderer>();
        if (groundRenderer == null)
        {
            Debug.LogError("No Renderer found on Ground!");
            return (Vector3.zero, Vector3.zero);
        }


        Vector3 groundCenter = groundRenderer.bounds.center; // Center position
        float groundWidth = groundRenderer.bounds.size.x; // Total width of the ground
        // float groundDepth = groundRenderer.bounds.size.z; // Depth of the ground

        // ✅ Find the farthest Z position (away from the camera)
        float farthestZ = groundRenderer.bounds.max.z;

        // Correctly split the ground along the X-axis
        Vector3 leftSide = groundCenter - new Vector3(groundWidth / 4, 0f, -farthestZ);
        Vector3 rightSide = groundCenter + new Vector3(groundWidth / 4, 0f, farthestZ);

        leftSide.y += 2f;
        rightSide.y += 2f;

        return (leftSide, rightSide);
    }

}