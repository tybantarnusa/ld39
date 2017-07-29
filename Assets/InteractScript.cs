using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject indicator;

    void Start()
    {
        indicator.SetActive(false);
    }

    void Update () {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag("Charger") || hitObject.CompareTag("Powerbank"))
            {
                if(Vector3.Distance(transform.position, hitObject.transform.position) < 2f) { 
                    indicator.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        if (hitObject.CompareTag("Charger"))
                        {
                            GetComponent<PlayerScript>().SetCharger(true);
                            Destroy(hitObject);

                        }
                        else if (hitObject.CompareTag("Powerbank"))
                        {
                            GetComponent<PlayerScript>().SetPowerbank(true);
                            Destroy(hitObject);
                        }
                    }
                }
            } else
            {
                indicator.SetActive(false);
            }
        }
    }
}
