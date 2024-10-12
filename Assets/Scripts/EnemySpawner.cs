using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnTime = 2f; 

    private List<Transform> spawnPoints = new List<Transform>(); 
    private Color[] primaryColors = { Color.red, Color.blue, Color.yellow }; 
    private void Start()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child); 
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) 
        {
            SpawnEnemy(); 
            yield return new WaitForSeconds(spawnTime); 
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Count == 0)
        {
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        Vector3 spawnAreaSize = spawnPoint.GetComponent<Renderer>().bounds.size;
        Vector3 spawnPosition = spawnPoint.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0, 
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Color randomColor = primaryColors[Random.Range(0, primaryColors.Length)];
        enemy.GetComponent<Renderer>().material.color = randomColor; 
    }
}
