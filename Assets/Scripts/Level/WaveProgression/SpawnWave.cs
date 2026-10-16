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

    private float timer = 0f;

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
        timer += Time.deltaTime;
        Debug.Log($"Timer: {timer}");
    }


    public void StartWave(int basicEnemies, int eliteEnemies, int companionEnemies, int bossEnemies, float distanceFromPlayer)
    {
        timer = 0f;
        int[] enemiesToSpawn = new int[] { basicEnemies, eliteEnemies, companionEnemies, bossEnemies };
        int enemyTypeIndex = 0;
        foreach (int enemyCount in enemiesToSpawn)
        {
            for (int i = 0; i < enemyCount;)
            {
                if(true)//timer >= 1f) // Wait for 1 second before spawning the next enemy
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
                i++;

                timer = 0f;
                }
            }
            enemyTypeIndex++;
        }
    }
}
