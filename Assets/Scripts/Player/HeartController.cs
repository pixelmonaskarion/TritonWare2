using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    public Image[] hearts;
    public int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHeartSprites();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHeartSprites()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }
}
