using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    public PlayerHealthSystem playerHealth;
    public Collider2D playerCollider;

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
    public void applyForce(Vector2 force)
    {
        playerCollider.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
        Debug.Log("Applied: " + force);
    }

}