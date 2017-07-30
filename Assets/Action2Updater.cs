using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action2Updater : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Texture[] textures = new Texture[3];

    void LateUpdate()
    {
        if (!player.GetComponent<PlayerScript>().HasPowerbank())
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<RawImage>().texture = textures[0];
        }
        else if (player.GetComponent<PlayerScript>().HasPowerbank() && !player.GetComponent<PlayerScript>().IsUsingPowerbank())
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<RawImage>().texture = textures[1];
        }
        else if (player.GetComponent<PlayerScript>().HasPowerbank() && player.GetComponent<PlayerScript>().IsUsingPowerbank())
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<RawImage>().texture = textures[2];
        }

        transform.GetChild(0).GetComponent<Slider>().value = Mathf.Lerp(transform.GetChild(0).GetComponent<Slider>().value, player.GetComponent<PlayerScript>().GetPowerbankPower(), 0.3f);
    }
}
