using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : CompoScript
{
    public Material LampOn; 
    public Material LampOff;
    private Renderer bulb;

    private void Start()
    {
        bulb = transform.Find("Bulb").gameObject.GetComponent<Renderer>();
    }
    public override void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            bulb.material = LampOn;
        }
    }
    public override void Deactivate() 
    {
        if (isActive)
        {
            isActive = false;
            bulb.material = LampOff;
        }
    }
}
