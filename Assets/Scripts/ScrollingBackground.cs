using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public float speed = 0.1f;

    void Start()
    {

    }

    void FixedUpdate()
    {

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * speed, 0);

    }
}
