using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class uiMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    public UnityEvent mouseEnterEvent;
    public UnityEvent mouseExitEvent;
    void Update()
    {
        if (mouse_over)
        {
            //Debug.Log("Mouse Over");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        mouseEnterEvent.Invoke();
        //Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        mouseExitEvent.Invoke();
        //Debug.Log("Mouse exit");
    }
}
