using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject gate;

    [SerializeField]
    private bool activating;

    [SerializeField]
    private GameObject requirement;

    [HideInInspector]
    public bool triggered;
    

    private void Start()
    {
        triggered = false;
        requirement = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (requirement == null)
        {
            TriggerGate(other);
        } else
        {
            if (requirement.GetComponent<GateTrigger>().triggered)
            {
                TriggerGate(other);
            }
        }
    }

    private void TriggerGate(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gate != null)
            {
                gate.SetActive(activating);
            }
        }
        triggered = true;
    }
}
