using Unity.VisualScripting;
using UnityEngine;

public class ScrollDown : MonoBehaviour
{
    public GameObject img1;
    public GameObject img2;

    public float scrollSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (img1 == null || img2 == null)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        img1.transform.position -= new Vector3(img1.transform.position.x, Time.deltaTime * scrollSpeed);
        img2.transform.position -= new Vector3(img2.transform.position.x, Time.deltaTime * scrollSpeed);
        if(img2.transform.localPosition.y < -0.8 && img2.transform.position.y < img1.transform.position.y)
        {
            Debug.Log("trigger");
            img2.transform.position = new Vector2(0, 5);
        }
        else if(img1.transform.localPosition.y < -0.8 && img1.transform.position.y < img2.transform.position.y)
        {
            img1.transform.position = new Vector2(0, 5);
        }

    }
}
