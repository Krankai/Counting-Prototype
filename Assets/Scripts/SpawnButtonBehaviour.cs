using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool ButtonPressed { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed = false;
    }
}
