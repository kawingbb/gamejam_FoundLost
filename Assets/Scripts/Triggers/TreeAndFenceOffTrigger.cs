using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAndFenceOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        base.BeginTrigger();
        GameManager.Instance.HideColor(ColorCode.TreeAndFence);
    }
}
