using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Player died");
        gameObject.SetActive(false);
    }
}