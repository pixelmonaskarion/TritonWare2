using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellActions : MonoBehaviour
{
    public int dmgMod = 0; // Adds damage
    public GameObject fireballPrefab;
    
    
    public bool dashRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            FireBallSpell();
        }
        // if (Keyboard.current.qKey.wasPressedThisFrame && !counterRunning)
        // {
        //     StartCoroutine(PlayerCounter(true));
        // }
        // if (Keyboard.current.eKey.wasPressedThisFrame && !counterRunning)
        // {
        //     StartCoroutine(PlayerCounter(false));
        // }
    }

    public void FireBallSpell()
    {
        Debug.Log("FIREBALL");
        Instantiate(fireballPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    // public void CounterRight()
    // {
    //     StartCoroutine(PlayerCounter(false));
    // }

    public void Dash()
    {
        if (dashRunning) return;
        StartCoroutine(PlayerDash());
    }

    IEnumerator PlayerDash()
    {
        dashRunning = true;
        gameObject.GetComponent<ApplyDamage>().invulnerable = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up*1600.0f);
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<ApplyDamage>().invulnerable = false;
        gameObject.tag = "Player";
        dashRunning = false;
    }

    public void AOEBlast()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            ApplyEnemyDamage enemyDamage;
            if (enemy.TryGetComponent<ApplyEnemyDamage>(out enemyDamage))
            {
                Vector2 delta = enemy.transform.position-gameObject.transform.position;
                if (delta.magnitude < 15f) {
                    enemyDamage.dealDamage(25);
                }
                enemyDamage.applyForce(delta.normalized * 1000f/delta.magnitude);
            }
        }
    }
}
