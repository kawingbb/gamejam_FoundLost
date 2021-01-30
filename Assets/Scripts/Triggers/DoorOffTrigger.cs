using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        GameManager.Instance.HideColor(ColorCode.Door);
    }
}
