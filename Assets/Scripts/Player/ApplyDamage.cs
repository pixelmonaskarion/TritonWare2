using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    public PlayerHealthSystem playerHealth;
    public Collider2D playerCollider;
    public Rigidbody2D playerRb;
    public bool invulnerable = false;
    public int dashDamage = 1;

    public AudioClip takeDMG;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        
    }

    public void dealDamage(float damage)
    {
        audio.clip = takeDMG;
        audio.Play();
        if (invulnerable) return;
        playerHealth.currentHealth -= damage;
        Debug.Log("Player Health -" + damage + " = " + playerHealth.currentHealth);
    }
    public void applyForce(Vector2 force)
    {
        if (invulnerable) return;
        playerCollider.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
        Debug.Log("Applied: " + force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && invulnerable)
        {
            ApplyEnemyDamage enemyDamage = other.GetComponent<ApplyEnemyDamage>();
            if (enemyDamage != null)
            {
                enemyDamage.dealDamage(dashDamage + GameObject.Find("Player").GetComponent<SpellActions>().dmgMod);
                enemyDamage.applyForce(playerRb.linearVelocity.normalized * 20f);
            }
            Destroy(gameObject);
        }
    }
    
}