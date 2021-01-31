using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Trigger
{
    public Dialogue dialogue;

    public override void BeginTrigger()
    {
        if (DialogueManager.Instance._isOpened) return;
        if (triggerOnlyOnce)
            enableInteract = false;
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
