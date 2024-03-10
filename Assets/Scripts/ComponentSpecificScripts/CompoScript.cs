using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompoScript : MonoBehaviour
{
    protected bool isActive = false;
    public bool isPower = false;
    public bool isSenseSensitive = false;
    abstract public void Activate();
    public abstract void Deactivate();
}
