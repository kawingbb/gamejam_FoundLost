using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        base.BeginTrigger();
        GameManager.Instance.HideColor(ColorCode.Grass);
    }
}
