using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(bool introduced)
    {
        Debug.Log("trigger dialogue");
        DialogueManager.Instance.StartDialogue(dialogue, this.gameObject, introduced);
    }

    public void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
    }
}
