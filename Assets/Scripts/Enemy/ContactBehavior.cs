using UnityEditor.UI;
using UnityEngine;

public class ContactBehavior : MonoBehaviour
{

    public int enemyContactType = 1;
    public float damageMod = 1f;
    public Collider2D target;
    private Collider2D self;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = GetComponent<Collider2D>();
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
        if(self.IsTouching(target))
        {
            Explode(50, 10);
            Destroy(gameObject);
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

    void Explode(int areaOfEffect, int damage)
    {
        if ((target.transform.position - self.transform.position).sqrMagnitude <= areaOfEffect * areaOfEffect)
        {
            Debug.Log("Enemy exploded!");
        }
    }
}
