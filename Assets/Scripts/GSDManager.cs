using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GSDManager : MonoBehaviour
{
    public static GSDManager Instance;
    public float hitPoints = 100f;
    public AudioSource source, soundtrack;
    public AudioClip fireSound, playerHitSound, alienHitSound, alienFire, thrust, SoundTrack;

    public GSDManager GetInstance() { return Instance; }
    public GameObject PauseMenuUI, GameOverMenuUI, scoreUI, healthUI, healthBar;

    public int score, enemies;
    public string nextSceneName;
    public float waitTime=3f;
    public bool music = true;
    public bool gamePaused = true;

    public enum GameState
    {
        menu, inGame, paused, gameOver
    }

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    public void StartGame()
    {
        PauseMenuUI.SetActive(false);
        GameOverMenuUI.SetActive(false);
        scoreUI.SetActive(true);
        healthUI.SetActive(true);
        healthBar.SetActive(true);
        gamePaused = false;

        Time.timeScale = 1;

        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        SetGameState(GameState.paused);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void EndTheScene()
    {
        SceneManager.LoadScene(0);
        //StartCoroutine(WaitAndLoadScene());
    }

    public IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(0);
    }

    public void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {

            GSDManager.Instance.source.mute = true;
            GSDManager.Instance.soundtrack.mute = true;

            SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(0);

        }
        else if (newGameState == GameState.inGame)
        {
            scoreUI.SetActive(true);
            healthUI.SetActive(true);
            healthBar.SetActive(true);
            SceneManager.LoadScene(1);
        }

        else if (newGameState == GameState.paused)
        {
            if (Time.timeScale == 1)
            {
                GSDManager.Instance.source.mute = true; 
                GSDManager.Instance.soundtrack.mute = true;
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0;

            }
            else if (Time.timeScale == 0)
            {
                GSDManager.Instance.source.mute = false;
                GSDManager.Instance.soundtrack.mute = false;
                PauseMenuUI.SetActive(false);
                Time.timeScale = 1;

            }
        }
        else if (newGameState == GameState.gameOver)
        {

            GameOverMenuUI.SetActive(true);


            if (Time.timeScale == 1)
            {

                Time.timeScale = 0;

            }

            GSDManager.Instance.source.mute = true;
            GSDManager.Instance.soundtrack.mute = true;
        }
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif
    }
}


