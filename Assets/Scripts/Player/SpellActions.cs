using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellActions : MonoBehaviour
{
    public int dmgMod = 0; // Adds damage
    public GameObject fireballPrefab;
    public GameObject meleePrefab;
    bool counterRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            FireBallSpell();
        }
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            MeleeAttackSpell();
        }
        if (Keyboard.current.qKey.wasPressedThisFrame && !counterRunning)
        {
            StartCoroutine(PlayerCounter(true));
        }
        if (Keyboard.current.eKey.wasPressedThisFrame && !counterRunning)
        {
            StartCoroutine(PlayerCounter(false));
        }
    }

    void FireBallSpell()
    {
        Instantiate(fireballPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
    void MeleeAttackSpell()
    {
        Instantiate(meleePrefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    IEnumerator PlayerCounter(bool flipped)
    {
        counterRunning = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up*800.0f);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Rigidbody2D>().AddForce((flipped ? -1f : 1f) * gameObject.transform.right*800.0f);
        counterRunning = false;
    }
}
