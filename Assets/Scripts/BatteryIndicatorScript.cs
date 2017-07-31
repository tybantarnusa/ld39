using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryIndicatorScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Texture[] textures = new Texture[5];

    [SerializeField]
    private Texture[] chargingTextures = new Texture[4];

    private float timer;
    private int chargingIndex;

    private void Start()
    {
        timer = 0f;
        chargingIndex = 0;
    }

    void LateUpdate() {
        PlayerScript _player = player.GetComponent<PlayerScript>();
        RawImage image = GetComponent<RawImage>();

        if (!_player.IsUsingCharger() && !_player.IsUsingPowerbank())
        {
            chargingIndex = 0;
            int battery = _player.GetBatteryLife();
            if (battery > 70)
            {
                image.texture = textures[3];
            }
            else if (battery > 50)
            {
                image.texture = textures[2];
            }
            else if (battery > 15)
            {
                image.texture = textures[1];
            }
            else if (battery > 0)
            {
                image.texture = textures[0];
            }
            else
            {
                image.texture = textures[4];
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 0.3f)
            {
                image.texture = chargingTextures[chargingIndex];
                chargingIndex = (chargingIndex + 1) % chargingTextures.Length;
                timer = 0f;
            }
        }
	}
}
