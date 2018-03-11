using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterPuzzle : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject rockPrefab;
    private float timeToSpawnTimer;
    public float timeBetweenWaves = 1f;

    public string LevelToLoad;

    void Update()
    {
        if (timeToSpawnTimer > 0)
            timeToSpawnTimer -= Time.deltaTime;
        else
        {
            SpawnRocks();
            timeToSpawnTimer = timeBetweenWaves;
        }

        if (Time.timeSinceLevelLoad >= 37)
        {
            Debug.Log("Time up");
            SceneManager.LoadScene(LevelToLoad);
        }
    }

    void SpawnRocks()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (randomIndex != i)
                Instantiate(rockPrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }
}