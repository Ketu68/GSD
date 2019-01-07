using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Image meterImage;
    public Text hpText;

	void Start () {

    }

	void Update () {

        meterImage.fillAmount = GSDManager.Instance.hitPoints / 100f;
        GameObject.Find("healthUI").GetComponent<Text>().text = "HEALTH : " + GSDManager.Instance.hitPoints;
    }
}
