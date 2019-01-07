using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveNPC : MonoBehaviour
{

    public GameObject zbullet, explosion;
    public float direction = 1.0f;
    public float timer;
    public AudioSource source, soundtrack;
    public AudioClip fireSound, playerHitSound, alienHitSound, alienFire, thrust, SoundTrack;

    private bool moving;
    public float speed=2f;
    public int newtarget;
    public float xPos, yPos;
    public float distance, startTime;
    public Vector3 startPosition, desiredPos;
    public int hp = 50;

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            moving = true;
            xPos = Random.Range(-4.5f, 4.5f);
            yPos = Random.Range(-4.5f, 4.5f);

            int rand = Random.Range(1, 4);

            switch(rand)
            {
                case 1:
                    {
                        desiredPos = new Vector3(xPos, transform.position.y, transform.position.z);
                        break;
                    }
                case 2:
                    {
                        desiredPos = new Vector3(transform.position.x, yPos, transform.position.z);
                        break;
                    }
                case 3:
                    {
                        desiredPos = new Vector3(xPos, yPos, transform.position.z);
                        break;
                    }
                case 4:
                    {
                        desiredPos = new Vector3(yPos, xPos, transform.position.z);
                        break;
                    }
                case 5:
                    {
                        desiredPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        break;
                    }
            }

            startPosition = transform.position;
            startTime = Time.time;
            timer = 0;
        }

        speed = 1f;

        if (moving) 
        {
            float timePassed = Time.time - startTime; 
            float distanceCovered = timePassed * speed;

            transform.position = Vector3.Lerp(startPosition, desiredPos, distanceCovered);

            if (distanceCovered >= 2)
            {
                moving = false;
            }
        }

        float shootrand = Random.Range(1, 140);

        if (shootrand == 3)  Shoot();

        Vector3 viewPortPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 viewPortXDelta = Camera.main.WorldToViewportPoint(transform.position + Vector3.left / 2);
        Vector3 viewPortYDelta = Camera.main.WorldToViewportPoint(transform.position + Vector3.up / 2);

        float deltaX = viewPortPosition.x - viewPortXDelta.x;
        float deltaY = viewPortPosition.y - viewPortYDelta.y;

        viewPortPosition.x = Mathf.Clamp(viewPortPosition.x, 0 + deltaX, 1 - deltaX);
        viewPortPosition.y = Mathf.Clamp(viewPortPosition.y, 0 + deltaY, 1 - deltaY);
        transform.position = Camera.main.ViewportToWorldPoint(viewPortPosition);
    }

    void Shoot()
    {
        GSDManager.Instance.source.PlayOneShot(GSDManager.Instance.alienFire, 1);
        GameObject b = (GameObject)(Instantiate(zbullet, transform.position, Quaternion.identity));
        b.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 200);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GSDManager.Instance.enemies--;
            Debug.Log("Enemies Left : " + GSDManager.Instance.enemies);

            GSDManager.Instance.source.PlayOneShot(GSDManager.Instance.playerHitSound, 1);
            GameObject exp = (GameObject)(Instantiate(explosion, transform.position, Quaternion.identity));
            Destroy(exp, .5f);
            Destroy(gameObject);
        }
    }
}
