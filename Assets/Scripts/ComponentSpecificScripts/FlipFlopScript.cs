using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : CompoScript
{
    private CableManager manager;

    private void Start()
    {
        manager = FindAnyObjectByType<CableManager>();
    }

    public override void Activate()
    {
        isActive = true;
    }
    public override void Deactivate()
    {
        isActive = false;
    }

    public override void Button()
    {
        blockMtoP = !blockMtoP;
        blockPtoM = !blockPtoM;
        manager.UpdateElectricThings();
    }

}
