using UnityEngine;

public class BleedEffect : MonoBehaviour
{
    private GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            gameObject.transform.position = target.transform.position;
        }
    }
}
