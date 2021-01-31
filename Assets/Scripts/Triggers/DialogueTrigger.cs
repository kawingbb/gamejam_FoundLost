using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Trigger
{
    public Dialogue dialogue;

    public override void BeginTrigger()
    {
        if (DialogueManager.Instance._isOpened) return;
        base.BeginTrigger();
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
