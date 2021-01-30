using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        GameManager.Instance.HideColor(ColorCode.Fire);
    }
}
