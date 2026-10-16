using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public float bulletSpeed;
    public float travelTime;
    public int damage = 50;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed;
        Destroy(gameObject, travelTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            ApplyEnemyDamage enemyDamage = other.GetComponent<ApplyEnemyDamage>();
            if (enemyDamage != null)
            {
                enemyDamage.dealDamage(damage + GameObject.Find("Player").GetComponent<SpellActions>().dmgMod);
                enemyDamage.applyForce(rb.linearVelocity.normalized * 20f);
            }
            Destroy(gameObject);
        }
    }

}
