using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public string startGameScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayAgain()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
        Scene SceneToLoad = SceneManager.GetSceneByName(startGameScene);
        SceneManager.LoadScene(startGameScene);
        SceneManager.SetActiveScene(SceneToLoad);
        SceneManager.UnloadSceneAsync(CurrentScene);
    }
}
