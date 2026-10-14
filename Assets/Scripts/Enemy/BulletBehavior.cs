using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float travelTime = 1f;
    public float bulletSpeed = 10f;
    private PlayerController playerController;
    private ApplyDamage applyDamage;
    private Rigidbody2D rb;
    private int _damage;
    private Collider2D playerCollider;
    public GameObject bulletHitPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            applyDamage = player.GetComponent<ApplyDamage>();
            playerController = player.GetComponent<PlayerController>();
            playerCollider = playerController.GetComponent<Collider2D>();
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("BulletBehavior requires a Rigidbody2D.", this);
            enabled = false;
            return;
        }

        rb.linearVelocity = transform.up * bulletSpeed;
        Destroy(gameObject, travelTime);
    }

    // FixedUpdate is called once per fixed frame rate
    void FixedUpdate()
    {
        if (applyDamage != null && playerController != null && rb != null)
        {
            if (playerCollider != null && rb.IsTouching(playerCollider))
            {
                PlayerHit();
            }
        }
    }

    public void Initialize(int damage)
    {
        _damage = damage;
    }

    private void PlayerHit()
    {
        if (bulletHitPrefab != null)
        {
            Instantiate(bulletHitPrefab, transform.position, Quaternion.identity * transform.rotation);
        }
        applyDamage.dealDamage(_damage);
        applyDamage.applyForce(rb.linearVelocity);
        Destroy(gameObject);
    }
}
