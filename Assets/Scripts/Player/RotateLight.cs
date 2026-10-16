using UnityEngine;

public class RotateLight : MonoBehaviour
{
    public GameObject lightPrefab;
    public GameObject target;
    public float rotationSpeed = 1f;
    private GameObject lightObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null || lightPrefab == null)
        {
            enabled = false;
        }
        lightObject = Instantiate(lightPrefab, target.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lightObject.transform.position = target.transform.position;
        lightObject.transform.Rotate(0f, 0f, rotationSpeed);
    }
}
