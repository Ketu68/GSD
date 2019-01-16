using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovingTargets : MonoBehaviour
{

    float timer = 0;
    public GameObject newObject;
    private int maxenemies = 12;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (!GSDManager.Instance.gamePaused)
        {
            timer += Time.deltaTime;
            float xrange = Random.Range(4, 22);
            float yrange = Random.Range(-10, 10);

            if (timer >= 1 && GSDManager.Instance.enemies < maxenemies)
            {
                Vector3 newPosition = new Vector3(GameObject.Find("gsdefender").transform.position.x + xrange, transform.position.y + yrange, 0);
                Instantiate(newObject, newPosition, Quaternion.identity);
                timer = 0;
                GSDManager.Instance.enemies++;
            }
        }
    }
}
