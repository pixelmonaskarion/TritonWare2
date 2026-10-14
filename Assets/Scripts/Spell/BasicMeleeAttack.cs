using UnityEngine;

public class BasicMeleeAttack : MonoBehaviour
{
    public float sweepAngle;
    public float attackDuration;
    float removeTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Rotate(new Vector3(0f, 0f, -sweepAngle/2f));
        removeTime = Time.time+attackDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, sweepAngle * Time.deltaTime/attackDuration));
        if (Time.time >= removeTime)
        {
            Destroy(gameObject);
        }
    }
}
