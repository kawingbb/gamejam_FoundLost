using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        base.BeginTrigger();
        GameManager.Instance.HideColor(ColorCode.Furniture);
    }
}
