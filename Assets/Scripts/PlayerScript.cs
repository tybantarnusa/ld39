using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    private GameObject batteryPercentageUI;

    public GameObject loseUI;
    public GameObject winUI;

    private bool hasCharger;
    private bool hasPowerbank;

    private bool usingFlashlight;

    private bool isCharging;
    private bool isChargingPortably;

    private int powerbankPower;

    private int battery;
    private float timer;

    private float dieTimer;

    private bool isWin;

    [SerializeField]
    private GameObject flashlight;
	
	private bool playLoseSoundOnce;

	void Start () {
        hasCharger = false;
        hasPowerbank = false;
        usingFlashlight = true;

        isCharging = false;
        isChargingPortably = false;

        battery = 75;

        timer = 0f;

        powerbankPower = 0;

        dieTimer = 0f;
		
		playLoseSoundOnce = false;
	}
	
	void Update () {
        timer += Time.deltaTime;

        HandleInput();
        if (!isWin)
            DischargingPhone();
        Charging();
        ChargingPortably();

        battery = Mathf.Clamp(battery, 0, 100);

         if (battery == 0)
        {
            dieTimer += Time.deltaTime;
        }

        if (dieTimer > 5)
        {
            GameObject.Find("Fader").GetComponent<RawImage>().color = new Color(0, 0, 0, Mathf.CeilToInt((dieTimer / 10f) * 255f));
        }

        if (dieTimer > 7f)
        {
            loseUI.SetActive(true);
            DisableControl();
            if (!transform.GetChild(1).GetComponent<AudioSource>().isPlaying && !playLoseSoundOnce)
            {
				playLoseSoundOnce = true;
                GetComponent<AudioSource>().Stop();
                transform.GetChild(1).GetComponent<AudioSource>().Play();
            }
            if (Input.anyKeyDown && !transform.GetChild(1).GetComponent<AudioSource>().isPlaying)
            {
                SceneManager.LoadScene("TitleScreen");
            }
        }

        if (isWin)
        {
            winUI.SetActive(true);
            DisableControl();
            GetComponent<AudioSource>().Stop();
        }
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

        if (isWin)
        {
            if (Input.anyKeyDown && !GameObject.Find("Win Trigger").GetComponent<AudioSource>().isPlaying)
            {
                SceneManager.LoadScene("TitleScreen");
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Alpha2) && hasCharger && powerbankPower > 0)
        {
            ChargePortably();
        }

    }

    private void DischargingPhone()
    {
        if (isCharging || isChargingPortably) return;

        batteryPercentageUI.GetComponent<Text>().color = Color.white;
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

        batteryPercentageUI.GetComponent<Text>().color = new Color(215, 234, 31);
        if (usingFlashlight && timer > 1)
        {
            battery++;
            timer = 0;
        }
        else if (!usingFlashlight && timer > 0.5f)
        {
            battery++;
            timer = 0;
        }
    }

    private void ChargingPortably()
    {
        if (!hasPowerbank || !isChargingPortably || powerbankPower <= 0) return;

        batteryPercentageUI.GetComponent<Text>().color = new Color(215, 234, 31);
        if (usingFlashlight && timer > 4)
        {
            if (timer > 2)
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
            transform.position = new Vector3(outlet.transform.GetChild(0).position.x, transform.position.y, outlet.transform.GetChild(0).position.z);
            transform.LookAt(outlet.transform);
        }
    }
    
    public void UnplugCharge()
    {
        isCharging = false;
        EnableControl();
    }

    public void DisableControl()
    {
        GetComponent<RigidbodyFirstPersonController>().enabled = false;
    }

    public void EnableControl()
    {
        GetComponent<RigidbodyFirstPersonController>().enabled = true;
    }

    public int GetBatteryLife()
    {
        return battery;
    }

    public void SetWin()
    {
        isWin = true;
    }
}
