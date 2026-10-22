using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    public int score;
    public int scoreCounter = 0;

    public TextMeshProUGUI scoreText;
    public GameObject playAgainButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCounter < score)
        {
            scoreCounter += 1;
            scoreText.text = $"Game Over\n" + "Score: " + scoreCounter;
        } else
        {
            playAgainButton.SetActive(true);
        }
    }
}
