using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Trigger
{
    public Dialogue dialogue;
    
    public override void BeginTrigger()
    {
        Debug.Log("Dialogue");
        enableInteract = false;
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
