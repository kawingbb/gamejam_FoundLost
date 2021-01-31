using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    public bool enableInteract = true;

    public bool triggerOnlyOnce = false;

    public virtual void BeginTrigger()
    {
        if (triggerOnlyOnce)
            enableInteract = false;
    }
}
