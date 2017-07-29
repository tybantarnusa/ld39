using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private bool hasCharger;
    private bool hasPowerbank;

    private bool usingFlashlight;

    private float battery;
    private float timer;

    [SerializeField]
    private GameObject flashlight;

	void Start () {
        hasCharger = false;
        hasPowerbank = false;
        usingFlashlight = true;

        battery = 100f;

        timer = 0f;
	}
	
	void Update () {
        HandleInput();
        //Debug.Log("Charger: " + hasCharger + " | Powerbank: " + hasPowerbank);

        timer += Time.deltaTime;
	    if (usingFlashlight)
        {

        }	
	}

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && usingFlashlight)
        {
            flashlight.GetComponent<Light>().intensity = 0.3f;
            usingFlashlight = false;
        } else if (Input.GetKeyDown(KeyCode.F) && !usingFlashlight)
        {
            flashlight.GetComponent<Light>().intensity = 1.8f;
            usingFlashlight = true;
        }
    }

    public void SetCharger(bool yes)
    {
        hasCharger = yes;
    }

    public void SetPowerbank(bool yes)
    {
        hasPowerbank = yes;
    }
}
