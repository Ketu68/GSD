using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public GameObject explosion, laser;
    public int enemiesleft;

    void Start()
    {
        Destroy(gameObject, 4);
    }

    void Update() { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {

            GSDManager.Instance.enemies--;
            enemiesleft = GSDManager.Instance.enemies;

            Debug.Log("Enemies Left : " + enemiesleft);

            GameObject exp = (GameObject)(Instantiate(explosion, transform.position, Quaternion.identity));
            Destroy(exp, .5f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject.Find("gsdefender").GetComponent<ManagePlayerHealth>().IncreaseScore();

            GSDManager.Instance.source.PlayOneShot(GSDManager.Instance.alienHitSound, 1);

        }
    }
}


