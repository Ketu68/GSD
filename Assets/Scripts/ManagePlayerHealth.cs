using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagePlayerHealth : MonoBehaviour
{

    public int score;
    public float hp;
    public Text hpText;
    public Image meterImage;
    public float startingHitPoints=96f, maxHitPoints=100f;

    public GameObject explosion, laser, newObject;

    //public enum GameState
    //{
    //    menu, inGame, paused, gameOver
    //}

    void Start()
    {
        score = 0;
        hp = GSDManager.Instance.hitPoints;
        meterImage.fillAmount = 1f;
        GameObject.Find("scoreUI").GetComponent<Text>().text = "SCORE : ";

    }

    void FixedUpdate()
    {
        meterImage.fillAmount = hp / 100f;
        hpText.text = "Health:" + (meterImage.fillAmount * 100f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "bullet")
        {
            GSDManager.Instance.source.PlayOneShot(GSDManager.Instance.playerHitSound, 1);
            DecreaseHealth();
        }
    }

    public void IncreaseScore()
    {
        score += 100;
        GameObject.Find("scoreUI").GetComponent<Text>().text = "SCORE : " + score;
    }

    public void DecreaseHealth()
    {
        if (GSDManager.Instance.hitPoints > 0)
        {
            StartCoroutine(Flicker());
            GSDManager.Instance.hitPoints -= 12;
        }
        else
        {
            Destroy(transform.gameObject);
            GSDManager.Instance.GameOver();
        }
    }

    public virtual IEnumerator Flicker()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public IEnumerator FreezeFor()
    {
         yield return new WaitForSeconds(4);
         StartOver();

            // Insert Game Over screen here.  Using main menu to test for now

         GameObject exp1 = (GameObject)(Instantiate(explosion, transform.position, Quaternion.identity));
         Destroy(exp1, 3f);

    }

    public void StartOver() {

        Destroy(transform.gameObject);

        GSDManager.Instance.source.Stop();
        GSDManager.Instance.soundtrack.Stop();
        GSDManager.Instance.EndTheScene();

    }
}
