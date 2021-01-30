using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOffTrigger : Trigger
{
    public override void BeginTrigger()
    {
        GameManager.Instance.HideColor(ColorCode.Rock);
    }
}
