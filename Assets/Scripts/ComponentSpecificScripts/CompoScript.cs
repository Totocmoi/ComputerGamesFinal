using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompoScript : MonoBehaviour
{
    protected bool isActive = false;
    public bool isPower = false;
    public bool blockMtoP = false;
    public bool blockPtoM = false;
    abstract public void Activate();
    public abstract void Deactivate();
    virtual public void Button() { }
    virtual public void Explode() { }
}
