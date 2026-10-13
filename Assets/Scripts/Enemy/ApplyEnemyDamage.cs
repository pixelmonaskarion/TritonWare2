using UnityEngine;

public class ApplyEnemyDamage : MonoBehaviour
{
    public BasicEnemyHealthSystem enemyHealth;
    public Collider2D enemyCollider;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void dealDamage(int damage)
    {
        enemyHealth.currentHealth -= damage;
        Debug.Log("Enemy Health -" + damage + " = " + enemyHealth.currentHealth);
    }
    public void applyForce(Vector2 force)
    {
        enemyCollider.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
        Debug.Log("Applied: " + force);
    }

}