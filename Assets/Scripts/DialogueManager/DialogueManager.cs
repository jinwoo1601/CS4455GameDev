﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Queue<string> sentences;
    public static DialogueManager Instance { get; private set; }

    public CanvasGroup MainCanvasGroup;
    public CanvasGroup OptionsButtonsCanvasGroup;
    public CanvasGroup NextButtonCanvasGroup;

    public Button NextButton;
    public Button AcceptButton;
    public Button RejectButton;

    private GameObject interactingNPC;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        MainCanvasGroup = GetComponent<CanvasGroup>();
        if (MainCanvasGroup == null)
            Debug.Log("Couldn't find canvas group for dialogue box");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AccetingCompanion ()
    {
        interactingNPC.SendMessage("Picked");
        EndDialogue();
    }

    public void StartDialogue(Dialogue dialogue, GameObject NPC)
    {
        interactingNPC = NPC;
        Debug.Log(interactingNPC.name);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach ( string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        showDialogueBox();
        showNextButton();

    }

    public void DisplayNextSentence()
    { 
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (sentences.Count == 1) 
        {
            hideNextButton();
            showOptionsButtons();
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        hideDialogueBox();
        hideOptionsButtons();
    }

    private void showDialogueBox()
    {
        MainCanvasGroup.alpha = 1.0f;
        MainCanvasGroup.blocksRaycasts = true;
        MainCanvasGroup.interactable = true;
    }

    private void hideDialogueBox()
    {
        MainCanvasGroup.alpha = 0.0f;
        MainCanvasGroup.blocksRaycasts = false;
        MainCanvasGroup.interactable = false;
    }

    private void hideNextButton()
    {
        NextButtonCanvasGroup.alpha = 0.0f;
        NextButtonCanvasGroup.blocksRaycasts = false;
        NextButtonCanvasGroup.interactable = false;
    }

    private void showNextButton()
    {
        NextButtonCanvasGroup.alpha = 1.0f;
        NextButtonCanvasGroup.blocksRaycasts = true;
        NextButtonCanvasGroup.interactable = true;
    }

    private void hideOptionsButtons()
    {
        OptionsButtonsCanvasGroup.alpha = 0.0f;
        OptionsButtonsCanvasGroup.blocksRaycasts = false;
        OptionsButtonsCanvasGroup.interactable = false;
    }

    private void showOptionsButtons()
    {
        OptionsButtonsCanvasGroup.alpha = 1.0f;
        OptionsButtonsCanvasGroup.blocksRaycasts = true;
        OptionsButtonsCanvasGroup.interactable = true;
    }

}
