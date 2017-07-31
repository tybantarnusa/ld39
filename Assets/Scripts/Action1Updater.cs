using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action1Updater : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Texture[] textures = new Texture[3];

    void LateUpdate () {
        if (!player.GetComponent<PlayerScript>().HasCharger())
        {
            GetComponent<RawImage>().texture = textures[0];
        }
        else if (player.GetComponent<PlayerScript>().HasCharger() && !player.GetComponent<PlayerScript>().IsUsingCharger())
        {
            GetComponent<RawImage>().texture = textures[1];
        }
        else if (player.GetComponent<PlayerScript>().HasCharger() && player.GetComponent<PlayerScript>().IsUsingCharger())
        {
            GetComponent<RawImage>().texture = textures[2];
        }
	}
}
