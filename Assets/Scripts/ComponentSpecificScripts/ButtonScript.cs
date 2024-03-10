using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler
{
    private CompoScript parent;
    void Start()
    {
        parent = transform.parent.GetComponent<CompoScript>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        parent.Button();
    }
}
