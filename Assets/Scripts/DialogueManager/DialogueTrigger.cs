﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        Debug.Log("trigger dialogue");
        DialogueManager.Instance.StartDialogue(dialogue, this.gameObject);
    }

    public void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
    }
}
