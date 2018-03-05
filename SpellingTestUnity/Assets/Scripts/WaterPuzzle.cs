using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterPuzzle : MonoBehaviour {

    public Transform[] spawnPoints;
    public GameObject rockPrefab;
    private float timeToSpawn = 4f;
    public float timeBetweenWaves = 1f;

    public string LeveltoLoad;

    // Use this for initialization
    void Update ()
    {
        if (Time.time >= timeToSpawn)
        {
            SpawnRocks();
            timeToSpawn = Time.time + timeBetweenWaves;
        }

        if (Time.time >= 37)
        {
            Debug.Log("Time up");
            SceneManager.LoadScene(LeveltoLoad);
        }
	}

    void SpawnRocks()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (randomIndex != i)
            {
                Instantiate(rockPrefab, spawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}