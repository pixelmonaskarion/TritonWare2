using UnityEngine;
using System.Collections.Generic;

public class EnemyTypes : MonoBehaviour
{
    [SerializeField] public Dictionary<int, GameObject> enemyPrefabs = new Dictionary<int, GameObject>();
    public GameObject basicEnemyTriangle, basicBlitzer, basicBomber, basicPoisonDartTriangle, basicSniper, wizardBoss;

    void Start()
    {
        enemyPrefabs.Add(0, basicEnemyTriangle);
        enemyPrefabs.Add(1, basicBlitzer);
        enemyPrefabs.Add(2, basicBomber);
        enemyPrefabs.Add(3, basicPoisonDartTriangle);
        enemyPrefabs.Add(4, basicSniper);
        enemyPrefabs.Add(5, wizardBoss);
    }
}
