using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAndFurnitureOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        base.BeginTrigger();
        GameManager.Instance.HideColor(ColorCode.WallAndFurniture);
    }
}
