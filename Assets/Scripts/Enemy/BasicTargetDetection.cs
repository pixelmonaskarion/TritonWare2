using UnityEngine;

public class BasicTargetDetection : MonoBehaviour
{

    public GameObject target;
    public float rotationSpeed;
    public float detectionRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = target.transform.position - transform.position;
        if (direction.sqrMagnitude == 0f)
        {
            return;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.RotateTowards
        (
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

    }
}
