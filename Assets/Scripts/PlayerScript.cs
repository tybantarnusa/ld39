using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    private GameObject batteryPercentageUI;

    private bool hasCharger;
    private bool hasPowerbank;

    private bool usingFlashlight;

    private bool isCharging;
    private bool isChargingPortably;

    private int powerbankPower;

    private int battery;
    private float timer;

    [SerializeField]
    private GameObject flashlight;

	void Start () {
        hasCharger = false;
        hasPowerbank = false;
        usingFlashlight = true;

        isCharging = false;
        isChargingPortably = false;

        battery = 75;

        timer = 0f;

        powerbankPower = 0;
	}
	
	void Update () {
        timer += Time.deltaTime;

        HandleInput();
        DischargingPhone();
        Charging();
        ChargingPortably();

    }

    private void LateUpdate()
    {
        batteryPercentageUI.GetComponent<Text>().text = battery + "%";
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && usingFlashlight)
        {
            flashlight.GetComponent<Light>().intensity = 0.1f;
            usingFlashlight = false;
        } else if (Input.GetKeyDown(KeyCode.F) && !usingFlashlight)
        {
            flashlight.GetComponent<Light>().intensity = 1.8f;
            usingFlashlight = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChargePortably();
        }
    }

    private void DischargingPhone()
    {
        if (isCharging || isChargingPortably) return;

        if (usingFlashlight && timer > 2)
        {
            battery--;
            timer = 0;
        }
        else if (!usingFlashlight && timer > 7)
        {
            battery--;
            timer = 0;
        }
    }

    private void Charging()
    {
        if (!hasCharger || !isCharging) return;

        if (usingFlashlight && timer > 5)
        {
            battery++;
            timer = 0;
        }
        else if (!usingFlashlight && timer > 2)
        {
            battery++;
            timer = 0;
        }
    }

    private void ChargingPortably()
    {
        if (!hasPowerbank || !isChargingPortably || powerbankPower <= 0) return;

        if (usingFlashlight && timer > 4)
        {
            if (timer > 10)
            {
                powerbankPower-=3;
                battery++;
                timer = 0;

                if (powerbankPower <= 0)
                {
                    isChargingPortably = false;
                }
            }
        }
        else if (!usingFlashlight && timer > 1)
        {
            powerbankPower--;
            battery++;
            timer = 0;
        }
    }

    public void SetCharger(bool yes)
    {
        hasCharger = yes;
    }

    public void SetPowerbank(bool yes)
    {
        hasPowerbank = yes;
        powerbankPower = (int)Random.Range(10, 35);
    }

    public bool HasCharger()
    {
        return hasCharger;
    }

    public bool HasPowerbank()
    {
        return hasPowerbank;
    }

    public bool IsUsingCharger()
    {
        return isCharging;
    }

    public bool IsUsingPowerbank()
    {
        return isChargingPortably;
    }

    public float GetPowerbankPower()
    {
        return (float)powerbankPower / 35f;
    }

    public void ChargePortably()
    {
        if (!isChargingPortably)
        {
            isChargingPortably = true;
            if (isCharging)
            {
                Charge(null);
            }
        }
        else
        {
            isChargingPortably = false;
        }
    }

    public void Charge(GameObject outlet)
    {
        if (!isCharging && outlet != null)
        {
            isCharging = true;
            isChargingPortably = false;
            DisableControl();
            transform.LookAt(outlet.transform);
        }
        else
        {
            isCharging = false;
            EnableControl();
        }
    }

    public void DisableControl()
    {
        GetComponent<RigidbodyFirstPersonController>().enabled = false;
    }

    public void EnableControl()
    {
        GetComponent<RigidbodyFirstPersonController>().enabled = true;
    }
}
