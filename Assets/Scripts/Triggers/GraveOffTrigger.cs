using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        GameManager.Instance.HideColor(ColorCode.Grave);
    }
}
