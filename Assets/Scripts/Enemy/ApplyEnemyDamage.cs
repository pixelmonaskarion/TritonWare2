using UnityEngine;

public class ApplyEnemyDamage : MonoBehaviour
{
    public BasicEnemyHealthSystem enemyHealth;
    public Collider2D enemyCollider;
    public GameObject deathEffectPrefab;
    private ScoreCounter scoreCounter;

    void Start()
    {
        scoreCounter = GameObject.FindGameObjectWithTag("Counter").GetComponent<ScoreCounter>();
    }

    void FixedUpdate()
    {
        
    }
    public void dealDamage(int damage)
    {
        enemyHealth.currentHealth -= damage;
        Debug.Log("Enemy Health -" + damage + " = " + enemyHealth.currentHealth);
        if (enemyHealth.currentHealth <= 0)
        {
            Debug.Log("Enemy has been defeated!");
            if (deathEffectPrefab != null)
            {
                Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            }
            scoreCounter.IncreaseScoreByEnemyValue(gameObject.name.Remove(gameObject.name.IndexOf('(')));
            Destroy(gameObject);
        }
    }
    public void applyForce(Vector2 force)
    {
        Rigidbody2D enemyRigidbody = enemyCollider.attachedRigidbody;
        BasicEnemyMovementController movementController =
            enemyRigidbody.GetComponent<BasicEnemyMovementController>();

        if (movementController != null)
        {
            movementController.ApplyKnockback(force);
        }
        else
        {
            enemyRigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        Debug.Log("Applied: " + force);
    }

}