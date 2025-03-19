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

        leftSide = CalculateGroundWidthThenSplit().leftSide;
        rightSide = CalculateGroundWidthThenSplit().rightSide;
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

    private void SpawnBoost()
    {
        Instantiate(boosts[Random.Range(0, boosts.Count)], leftSide, Quaternion.identity);
        Instantiate(boosts[Random.Range(0, boosts.Count)], rightSide, Quaternion.identity);
    }

    (Vector3 leftSide, Vector3 rightSide) CalculateGroundWidthThenSplit()
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

        float groundWidth = groundRenderer.bounds.size.x;
        Vector3 groundCenter = groundRenderer.bounds.center;

        Vector3 leftSide = groundCenter - new Vector3(groundWidth / 4, -2f, 0);
        Vector3 rightSide = groundCenter + new Vector3(groundWidth / 4, 2f, 0);

        return (leftSide, rightSide);
    }


}