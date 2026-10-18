using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnWave : MonoBehaviour
{
    public GameObject player;

    public GameObject spawnEffectPrefab;

    public GameObject basicEnemyPrefab;
    public GameObject eliteEnemyPrefab;
    public GameObject companionEnemyPrefab;
    public GameObject bossEnemyPrefab;


    private Dictionary<int, GameObject> enemyPrefabs = new Dictionary<int, GameObject>();
    private int enemyTypeIndex = 0;
    public int distanceFromPlayer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyPrefabs.Add(0, basicEnemyPrefab);
        enemyPrefabs.Add(1, eliteEnemyPrefab);
        enemyPrefabs.Add(2, companionEnemyPrefab);
        enemyPrefabs.Add(3, bossEnemyPrefab);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }


    public void StartWave(int basicEnemies, int eliteEnemies, int companionEnemies, int bossEnemies)
    {
        int[] enemiesToSpawn = new int[] { basicEnemies, eliteEnemies, companionEnemies, bossEnemies };
        enemyTypeIndex = 0;
        StartCoroutine(SpawnRoutine(enemiesToSpawn));
    }

    private IEnumerator<WaitForSeconds> SpawnRoutine(int[] enemiesToSpawn)
    {
        foreach (int enemyCount in enemiesToSpawn)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                spawnEnemy(enemyTypeIndex, distanceFromPlayer);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(3f);
            enemyTypeIndex++;
        }
    }

    void spawnEnemy(int enemyTypeIndex, float distanceFromPlayer)
    {
        float xPos, yPos;
        float angle = Mathf.Deg2Rad * Random.Range(0f, 360f);
        float radius = Random.Range(distanceFromPlayer - 10f, distanceFromPlayer + 10f);

        float randomX = Mathf.Cos(angle) * radius;
        float randomY = Mathf.Sin(angle) * radius;

        xPos = player.transform.position.x + randomX;
        yPos = player.transform.position.y + randomY;

        Instantiate(enemyPrefabs[enemyTypeIndex], new Vector3(xPos, yPos), Quaternion.identity);
        Instantiate(spawnEffectPrefab, new Vector3(xPos, yPos), Quaternion.identity);
        Debug.Log($"Spawning enemy type {enemyTypeIndex} at ({xPos}, {yPos})");
    }
}
