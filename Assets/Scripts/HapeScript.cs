using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapeScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Texture[] textures = new Texture[5];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerScript _player = player.GetComponent<PlayerScript>();
        Material mat = GetComponent<MeshRenderer>().material;

        int battery = _player.GetBatteryLife();

        if (battery > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (battery > 70)
        {
            mat.mainTexture = textures[4];
        }
        else if (battery > 50)
        {
            mat.mainTexture = textures[3];
        }
        else if (battery > 15)
        {
            mat.mainTexture = textures[2];
        }
        else if (battery > 0)
        {
            mat.mainTexture = textures[1];
        }
        else
        {
            mat.mainTexture = textures[0];
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
