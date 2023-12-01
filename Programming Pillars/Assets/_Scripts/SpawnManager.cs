using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Transform playerT;

    [SerializeField] private float spawnRadius;
    [SerializeField] private float minSpawnRadius;
    [SerializeField] private float maxSpawnRadius;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float spawnTimeReduction;






    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(SpawnEnemies());
    }





    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(10f);
        while (!GameManager.gameMan.gameOver)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], FindSpawnPosition(), Quaternion.identity);
            ReduceSpawnTime();
            yield return new WaitForSeconds(spawnDelay);
        }
    }



    private void ReduceSpawnTime()
    {
        spawnDelay -= spawnTimeReduction;
        if (spawnDelay < minSpawnTime) spawnDelay = minSpawnTime;
    }

    public Vector3 FindSpawnPosition()
    {
        spawnRadius = Random.Range(minSpawnRadius, maxSpawnRadius);
        float angle = Random.Range(0f, 360f);

        Vector3 spawnPos = new Vector3();
        spawnPos.x = playerT.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius;
        spawnPos.y = 0f;
        spawnPos.z = playerT.position.z + Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius;
        return spawnPos;
    }
}
