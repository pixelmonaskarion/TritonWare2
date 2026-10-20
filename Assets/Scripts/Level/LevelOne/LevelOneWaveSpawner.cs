using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOneWaveSpawner : MonoBehaviour
{
    private SpawnWave _spawnWave;
    private int basic, elite, companion, boss = 0;
    private int wave = 0;
    private int variety = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnWave = GetComponent<SpawnWave>();
        _spawnWave.distanceFromPlayer = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Count() == 0)
        {
            Debug.Log("Spawning Wave: " + wave);
            wave++; variety++;
            basic = wave + Random.Range(-1 * variety, variety); elite = (wave / 2) + Random.Range(-1 * variety / 2, variety / 2); companion = (wave / 3) + Random.Range(-1 * variety / 3, variety / 3);
            if (wave % 5 == 0) { boss = 1; }
            _spawnWave.StartWave(basic, elite, companion, boss);
        }
    }
}