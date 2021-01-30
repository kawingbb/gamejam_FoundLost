using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        GameManager.Instance.HideColor(ColorCode.Item);
    }
}
