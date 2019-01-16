using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Awake()
    {

    }

    void Start()
    {
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(0);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
