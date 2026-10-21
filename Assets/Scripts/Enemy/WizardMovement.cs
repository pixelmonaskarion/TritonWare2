using UnityEngine;
using System.Collections.Generic;

public class WizardMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private ApplyEnemyDamage applyEnemyDamage;
    public float dashChance = 0;
    public float dashStrength = 10f;
    public float fireballChance = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
                yield return new WaitForSeconds(2f);
            }
            else
            {
                dashChance += 10;
            }

            // Big FireBall Check
            if (Random.Range(0, 100) <= fireballChance)
            {
                BigFireball();
                fireballChance = 0;
                yield return new WaitForSeconds(2f);
            }
            else
            {
                fireballChance += 10;
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
        Debug.Log("Tried to fireball");
    }

}
