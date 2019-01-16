using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public bool isFollowing;
    public float xOffset, yOffset;

	void Start () {
        //player = FindObjectOfType<gsdefender>();
        isFollowing = true;
	}
	
	void Update () {
		if(isFollowing)
        {
            transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);
        }
	}
}
