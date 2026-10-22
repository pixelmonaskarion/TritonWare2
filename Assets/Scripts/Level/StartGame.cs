using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    public string nextScene = "Game";
    private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.enabled = true;
        button.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
        Scene SceneToLoad = SceneManager.GetSceneByName(nextScene);
        SceneManager.LoadScene(nextScene);
        SceneManager.SetActiveScene(SceneToLoad);
        SceneManager.UnloadSceneAsync(CurrentScene);
    }
}
