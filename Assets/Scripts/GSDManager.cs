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
    public GameState currentGameState = GameState.menu;
    public GameObject MainMenuUI, OptionsMenuUI, HelpMenuUI, PauseMenuUI, GameOverMenuUI;

    public int score, enemies;
    public string nextSceneName;
    public float waitTime=3f;
    public bool music = true;

    public enum GameState
    {
        menu, inGame, paused, gameOver
    }

    public void Awake()
    {
        Instance = this;
        currentGameState = GameState.menu;
    }

    void Start()
    {

    }

    public void StartGame()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);
        HelpMenuUI.SetActive(false);
        GameOverMenuUI.SetActive(false);
        MainMenuUI.SetActive(false);

        Time.timeScale = 1;

        SetGameState(GameState.inGame);
        SceneManager.LoadScene(0);
    }
    public void PauseGame()
    {
        SetGameState(GameState.paused);


    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        //SceneManager.LoadScene(0);
    }
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
        //SceneManager.LoadScene(0);
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
            PauseMenuUI.SetActive(false);
            OptionsMenuUI.SetActive(false);
            HelpMenuUI.SetActive(false);
            GameOverMenuUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }
        else if (newGameState == GameState.inGame)
        {
            PauseMenuUI.SetActive(false);
            OptionsMenuUI.SetActive(false);
            HelpMenuUI.SetActive(false);
            GameOverMenuUI.SetActive(false);
            MainMenuUI.SetActive(false);
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
            PauseMenuUI.SetActive(false);
            OptionsMenuUI.SetActive(false);
            HelpMenuUI.SetActive(false);
            GameOverMenuUI.SetActive(true);
            MainMenuUI.SetActive(false);

            if (Time.timeScale == 1)
            {

                Time.timeScale = 0;

            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            GSDManager.Instance.source.mute = true;
            GSDManager.Instance.soundtrack.mute = true;
        }
        currentGameState = newGameState;
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


