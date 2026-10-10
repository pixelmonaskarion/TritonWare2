using UnityEngine;

public class BasicTargetDetection : MonoBehaviour
{

    public GameObject target;
    public float rotationSpeed;
    public float detectionRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = GetTargetRotation();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = GetTargetRotation();
        transform.rotation = Quaternion.RotateTowards
        (
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

    }

    Quaternion GetTargetRotation()
    {
        if (target == null)
        {
            return Quaternion.identity;
        }

        Vector3 direction = target.transform.position - transform.position;
        if (direction.sqrMagnitude == 0f)
        {
            return Quaternion.identity;
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        return targetRotation;
    }
}
