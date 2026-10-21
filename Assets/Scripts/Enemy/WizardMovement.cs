using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WizardMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private ApplyEnemyDamage applyEnemyDamage;
    public float dashChance = 0;
    public float dashStrength = 10f;
    public float fireballChance = 0;
    public GameObject fireballPrefab;
    private ShootBullet shootBullet;
    public float spawnChance = 0;
    private SpawnWave spawnMinions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootBullet = GetComponent<ShootBullet>();
        applyEnemyDamage = GetComponent<ApplyEnemyDamage>();
        if (rb == null || applyEnemyDamage == null)
        {
            enabled = false;
        }
        StartCoroutine(SpecialMoves());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator<WaitForSeconds>SpecialMoves()
    {
        while (gameObject == enabled)
        {
            Debug.Log(dashChance + " " + fireballChance);
            // Dash Check
            if (Random.Range(0, 100) <= dashChance)
            {
                Debug.Log("Wizard used Dash");
                Dash();
                dashChance = 0;
            }
            else
            {
                dashChance += 20;
            }

            // Big FireBall Check
            if (Random.Range(0, 100) <= fireballChance)
            {
                BigFireball();
                fireballChance = 0;
            }
            else
            {
                fireballChance += 30;
            }

            if (Random.Range(0, 100) <= spawnChance && GameObject.FindGameObjectsWithTag("Enemy").Count() <= 10)
            {
                spawnMinions = GetComponent<SpawnWave>();
                spawnMinions.player = gameObject;
                spawnMinions.distanceFromPlayer = 10;
                spawnMinions.StartWave(Random.Range(0, 4), Random.Range(0, 3), -10, -10);
                spawnChance = 0;
                yield return new WaitForSeconds(5f);
            }
            else
            {
                spawnChance += 5;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void Dash()
    {
        if (Random.Range(0, 10) >= 5)
        { applyEnemyDamage.applyForce(transform.right * dashStrength); }
        else
        { applyEnemyDamage.applyForce(-transform.right * dashStrength); }
    }

    void BigFireball()
    {
        Instantiate(fireballPrefab, shootBullet.spawnPoint.position, shootBullet.spawnPoint.rotation);
    }

}
