using UnityEngine;
using System.Collections.Generic;

public class PlayerHealthSystem : MonoBehaviour
{
    public HealthBarController healthBarController;
    public float currentHealth;
    public float maxHealth;

    private PlayerController playerController;

    void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
        healthBarController.SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            GameOver();
        }
        healthBarController.SetHealth(currentHealth);
    }

    void GameOver()
    {
        Debug.Log("Player died");
        gameObject.SetActive(false);
    }

    public void Bleed(int duration, int damagePerSecond)
    {
        StartCoroutine(dealDamage(damagePerSecond, duration));
    }

    public void Poison(int duration, int damagePerSecond)
    {
        playerController.isPoisoned = true;
        Debug.Log("Player is poisoned");
        StartCoroutine(dealDamage(damagePerSecond, duration));
    }

    private IEnumerator<WaitForSeconds> dealDamage(int damage, int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            currentHealth -= damage;
            Debug.Log("Player Health -" + damage + " = " + currentHealth);
            yield return new WaitForSeconds(1f);
        }
        if (playerController.isPoisoned)
        {
            playerController.isPoisoned = false;
            Debug.Log("Player is no longer poisoned");
        }
    }

    
}