using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WizardFireball : MonoBehaviour
{
    public float bulletSpeed;
    public float travelTime;
    public float damage = 50;
    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    private GameObject player;
    private float time = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (explosionPrefab == null)
        {
            Debug.Log("Missing wizard fireball explosion prefab");
            enabled = false;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > travelTime || rb.IsTouching(player.GetComponent<Collider2D>()))
        {
            Explode();
        }
    }

    void Explode()
    {
        ApplyDamage playerApplyDamage = player.GetComponent<ApplyDamage>();

        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (!(damage / distanceToPlayer <= 5))
        {
            playerApplyDamage.dealDamage(damage / distanceToPlayer);
            Debug.Log("Wizard did " + (damage / distanceToPlayer) + " damage.");

            playerApplyDamage.applyForce(rb.linearVelocity.normalized * 20f);            
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }

}
