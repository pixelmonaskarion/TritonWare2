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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        applyDamage = FindAnyObjectByType<PlayerController>().GetComponent<ApplyDamage>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        Debug.Log("Place Holder: Do dash attack");
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
            applyDamage.dealDamage(damage);
            applyDamage.applyForce(rb.linearVelocity);
            Destroy(gameObject);
        }
    }
}
