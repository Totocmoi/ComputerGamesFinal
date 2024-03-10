using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BorneScript : MonoBehaviour, IPointerDownHandler
{
    private CableManager CableManager;
    // Start is called before the first frame update
    void Start()
    {
        CableManager = FindAnyObjectByType<CableManager>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked on " + eventData.pointerCurrentRaycast.gameObject.name+ " of " + eventData.pointerCurrentRaycast.gameObject.transform.parent.name);
        CableManager.AddPoint(eventData.pointerCurrentRaycast.gameObject);
    }

    
}
