using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject gateObject;

    public void ToggleGate()
    {
        if (gateObject.activeInHierarchy)
        {
            gateObject.SetActive(false);
        }
        else
        {
            gateObject.SetActive(true);
        }
    }

    public bool IsOpened()
    {
        return !gateObject.activeInHierarchy;
    }
}
