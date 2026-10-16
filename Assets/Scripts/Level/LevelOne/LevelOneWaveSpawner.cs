using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOneWaveSpawner : MonoBehaviour
{
    private SpawnWave _spawnWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnWave = GetComponent<SpawnWave>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("Spawning wave 1");
            _spawnWave.distanceFromPlayer = 30;
            _spawnWave.StartWave(5, 5, 5, 5);
        }
    }
}