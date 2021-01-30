using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    public bool enableInteract = true;
    
    public abstract void BeginTrigger();
}
