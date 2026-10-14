using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyHealth : MonoBehaviour
{
    public float health = 2f;
    Rigidbody2D body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerAttack"))
        {
            Rigidbody2D colliderBody = collider.gameObject.GetComponent<Rigidbody2D>();
            Vector3? relativeVelocity = null;
            if (colliderBody != null)
            {
                relativeVelocity = colliderBody.linearVelocity-body.linearVelocity;
            }
            if (relativeVelocity == null || relativeVelocity == new Vector3(0f, 0f, 0f))
            {
                relativeVelocity = collider.gameObject.transform.position-gameObject.transform.position;
            }
            body.linearVelocity = Vector3.zero;
            body.AddForce(((Vector3)relativeVelocity).normalized*400.0f);
            health -= 1f;
        }
    }
}
