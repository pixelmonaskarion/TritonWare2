using UnityEngine;

public class OnHitEffect : MonoBehaviour
{
    private GameObject target;
    public BleedEffect BleedEffect;
    public PoisonEffect PoisonEffect;
    public string effectType = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == target.GetComponent<Collider2D>())
        {
            ApplyEffect();
        }
    }

    void ApplyEffect()
    {
        switch (effectType)
        {
            case "bleed":
                Instantiate(BleedEffect, target.transform.position, Quaternion.identity);
                break;
            case "poison":
                Instantiate(PoisonEffect, target.transform.position, Quaternion.identity);
                break;
        }
    }
}
