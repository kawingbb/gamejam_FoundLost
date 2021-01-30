using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Trigger
{
    public Dialogue dialogue;
    
    public override void BeginTrigger()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
