using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessScript : CompoScript
{
    public override void Activate()
    {
        isActive = true;
    }
    public override void Deactivate()
    {
        isActive = false;
    }
}
