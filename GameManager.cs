using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class GSDManager : MonoBehaviour
{
    public AudioClip fireSound, hitSound;

    // This lets other scripts in the scene find this instance without searching.
    static GameManager _instance;
    public GameManager GetInstance() { return _instance; }

    // Inspector variables so we can view our score, and configure scene loading.
    public int score;
    public string nextSceneName;
    public float waitTime=3f;

    // Ensure the instance is ready by the time it's needed.
    public void Awake()
    {
        _instance = this;
    }

    public void EndTheScene()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    public IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(waitTime);
 //       SceneManager.LoadScene(nextSceneName);
        SceneManager.LoadScene(0);
    }

    public static PlaySound(int effect)
    {
        switch (effect)
        {
            case 1:
                {
                    GetComponent<AudioSource>().clip = fireSound;
                    GetComponent<AudioSource>().Play();
                }
            case 2:
                {
                    GetComponent<AudioSource>().clip = hitSound;
                    GetComponent<AudioSource>().Play();
                }
        }
    }
}
