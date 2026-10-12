using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float travelTime = 1f;
    public float bulletSpeed = 10f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.forward * bulletSpeed;
    }

    // FixedUpdate is called once per fixed frame rate
    void FixedUpdate()
    {
         Destroy(gameObject, travelTime);
    }
}
