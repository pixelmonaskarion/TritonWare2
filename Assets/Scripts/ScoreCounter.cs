using UnityEngine;
using System.Collections.Generic;
using System;

public class ScoreCounter : MonoBehaviour
{
    public float score = 0f;
    public bool survivedBossWave = false;

    private Dictionary<string, int> enemyValues = new Dictionary<string, int>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyValues.Add("BasicEnemyTriangle", 100);
        enemyValues.Add("BasicBlitzer", 150);
        enemyValues.Add("BomberTriangle", 100);
        enemyValues.Add("PoisonDartTriangle", 200);
        enemyValues.Add("BasicSniper", 300);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScoreByWaveSurvived(int waveCount)
    {
        if (waveCount >= 1)
        {
            if (survivedBossWave) // After a Boss wave
            {
                score += waveCount * 800;
                Debug.Log("Survived boss wave for " + waveCount * 800 + " points.");
                return;
            }
            Debug.Log("Survived wave " + waveCount + " for " + waveCount * 400 + " points.");
            score += waveCount * 400;
        }
    }
    public void IncreaseScoreByEnemyValue(string enemyType)
    {
        Debug.Log("Defeated " + enemyType + " for " + enemyValues[enemyType] + " points.");
        score += enemyValues[enemyType];
    }
}
