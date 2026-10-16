using UnityEditor.UI;
using UnityEngine;

public class ContactBehavior : MonoBehaviour
{

    public int enemyContactType = 1;
    public float damageMod = 1f;
    public Collider2D target;
    private Collider2D self;
    private Rigidbody2D rb;
    private ApplyDamage applyDamage;

    public GameObject BomberExplosionPrefab;
    public GameObject BleedPrefab;
    public BasicEnemyMovementController enemyMovement;

    private float timeSinceLastAttack = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Collider2D>();
        applyDamage = FindAnyObjectByType<PlayerController>().GetComponent<ApplyDamage>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeSinceLastAttack <= 30f)
        {
            timeSinceLastAttack += Time.fixedDeltaTime;
        }

        if (target != null) {
            switch(enemyContactType)
            {
                case 1:
                Bomber();
                break;

                case 2:
                Blitzer();
                break;

                default:
                Basic();
                break;
            }
        }
    }

    void Bomber()
    {
        if((target.transform.position - self.transform.position).sqrMagnitude <= 2f * 2f)
        {
            Explode(50f, 10);
        }
    }

    void Blitzer()
    {
        if((target.transform.position - self.transform.position).sqrMagnitude <= 4f * 4f)
        {
            MeleeAttack(10, 1f);
        }
    }

    void Basic()
    {
        Debug.Log("Apply melee damage");
    }

    void Explode(float areaOfEffect, int damage)
    {
        if ((target.transform.position - self.transform.position).sqrMagnitude <= areaOfEffect * areaOfEffect)
        {
            Debug.Log("Enemy exploded!");
            Instantiate(BomberExplosionPrefab, self.transform.position, Quaternion.identity);
            applyDamage.dealDamage(damage);
            applyDamage.applyForce(rb.linearVelocity);
            Destroy(gameObject);
        }
    }

    void MeleeAttack(int damage, float cooldown)
    {
        if ((target.transform.position - self.transform.position).sqrMagnitude <= 3f * 3f && timeSinceLastAttack >= cooldown)
        {
            Debug.Log("Enemy melee attacked!");
            applyDamage.dealDamage(damage);
            applyDamage.applyForce(rb.linearVelocity.normalized * 10f);
            enemyMovement.ApplyKnockback(-rb.linearVelocity.normalized * 20f);
            ApplyEffect("bleed");
            timeSinceLastAttack = 0;
        }
    }

    void ApplyEffect(string effectName)
    {
        switch (effectName)
        {
            case "bleed":
                Instantiate(BleedPrefab, target.transform.position, Quaternion.identity * transform.rotation);
                break;
            default:
                Debug.Log("No effect applied.");
                break;
        }
    }
}
