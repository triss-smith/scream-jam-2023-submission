using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject prefabToInstantiate;  // The prefab to be instantiated.
    public float distanceFromPlayer = 2.0f; // The distance away from the player to instantiate the prefab.

    private Transform playerTransform;       // Reference to the player's transform.
    private bool hasSpawnedPrefab = false;   // Flag to track if the prefab has been instantiated.

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawnedPrefab && other.CompareTag("Player"))
        {
            // Calculate the position to instantiate the prefab.
            Vector3 spawnPosition = playerTransform.position - new Vector3(distanceFromPlayer, 0, 0);

            // Instantiate the prefab at the calculated position.
            Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

            hasSpawnedPrefab = true; // Set the flag to indicate the prefab has been instantiated.
        }
    }
}
