using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    public PlayerHealthSystem playerHealth;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void dealDamage(int damage)
    {
        playerHealth.currentHealth -= damage;
        Debug.Log("Player Health -" + damage + " = " + playerHealth.currentHealth);
    }


}