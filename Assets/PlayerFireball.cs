using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public float bulletSpeed;
    public float travelTime;
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
}
