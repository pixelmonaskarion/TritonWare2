using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartyMode : MonoBehaviour
{
    private SpawnWave _spawnWave;
    private int basic, elite, challenge, boss = 0;
    public int wave = 0;
    private int variety = 0;
    private ScoreCounter scoreCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreCounter = GameObject.FindGameObjectWithTag("Counter").GetComponent<ScoreCounter>();
        _spawnWave = GetComponent<SpawnWave>();
        _spawnWave.distanceFromPlayer = 30;
        StartCoroutine(CheckForSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator<WaitForSeconds> CheckForSpawn()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Count() == 0)
            {
                if (wave % 6 == 1 && wave != 1)
                {
                    scoreCounter.survivedBossWave = true;
                }
                wave++; variety++;
                Debug.Log("Spawning Wave: " + wave);
                
                wave += 3;
                basic = wave + Random.Range(-1 * variety, variety); 
                elite = (wave / 2) + Random.Range(-1 * variety / 2, variety / 2); 
                challenge = (wave / 3) + Random.Range(-1 * variety / 3, variety / 3);
                wave -= 3;
                if (wave % 6 == 0) { boss = 1; }

                _spawnWave.StartWave(basic, elite, challenge, boss);

                scoreCounter.IncreaseScoreByWaveSurvived(wave);

                scoreCounter.survivedBossWave = false;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}