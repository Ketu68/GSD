using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{

    public GameObject laser, explosion;
    public bool facingright = true;
    public string firedirection = "right";
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GSDManager.Instance.PauseGame();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(!GSDManager.Instance.source.isPlaying) GSDManager.Instance.source.PlayOneShot(GSDManager.Instance.thrust, 1);
            gameObject.transform.Translate(Vector3.left * 0.2f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!GSDManager.Instance.source.isPlaying) GSDManager.Instance.source.PlayOneShot(GSDManager.Instance.thrust, 1);
            gameObject.transform.Translate(Vector3.right * 0.2f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
             gameObject.transform.Translate(Vector3.up * 0.2f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
             gameObject.transform.Translate(Vector3.down * 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GSDManager.Instance.source.PlayOneShot(GSDManager.Instance.fireSound, 1);

            if (facingright)
            {
                GameObject b = (GameObject)(Instantiate(laser, transform.position + transform.right * 2.7f, Quaternion.identity));
                b.GetComponent<Rigidbody2D>().AddForce(transform.right * 1800);
            }
            else
            {
                GameObject b = (GameObject)(Instantiate(laser, transform.position + transform.right * -2.7f, Quaternion.identity));
                b.GetComponent<Rigidbody2D>().AddForce(transform.right * -1800);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GSDManager.Instance.QuitGame();
        }

        Vector3 viewPortPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 viewPortXDelta = Camera.main.WorldToViewportPoint(transform.position + Vector3.left / 2);
        Vector3 viewPortYDelta = Camera.main.WorldToViewportPoint(transform.position + Vector3.up / 2);

        float deltaX = viewPortPosition.x - viewPortXDelta.x;
        float deltaY = viewPortPosition.y - viewPortYDelta.y;

        viewPortPosition.x = Mathf.Clamp(viewPortPosition.x, 0 + deltaX, 1 - deltaX);
        viewPortPosition.y = Mathf.Clamp(viewPortPosition.y, 0 + deltaX, 1 - deltaX);
        transform.position = Camera.main.ViewportToWorldPoint(viewPortPosition);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "bullet")
        {
            GameObject exp = (GameObject)(Instantiate(explosion, transform.position, Quaternion.identity));
            Destroy(exp, .5f);
            Destroy(collision.gameObject);
        }
    }

    private void Flip()
    {
        facingright = !facingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
