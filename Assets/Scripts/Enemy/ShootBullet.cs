using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float fireRate = 5f;
    public int bulletDamage = 10;
    private float _nextFireTime;

    public BasicTargetDetection targetCheck;
    private AudioSource audio;
    public AudioClip shootSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetCheck = GetComponent<BasicTargetDetection>();
        audio = GetComponent<AudioSource>();
        _nextFireTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetCheck.AwareOfPlayer)
        {
            if (Time.time >= _nextFireTime && bulletPrefab != null && spawnPoint != null)
            {
                EnemyShoot();
            }
        }
        else
        {
            //Debug.Log("Enemy is not aware of player.");
        }
    }

    void EnemyShoot()
    {
        audio.clip = shootSound;
        audio.Play();
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();
        bulletBehavior.Initialize(bulletDamage);
        _nextFireTime = Time.time + 1f / fireRate;
    }
}
