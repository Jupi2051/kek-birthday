using Assets.Scripts.OOPWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueLines))]
public class NPC : MonoBehaviour, IInteractable
{
    public StateManager stateManager;
    public int CurrentReplyIndex = 0;
    public bool ResetConversationOnEnd = true;
    public float ToolTipHeight = 0.5f;
    public SpeechUnits Dialogus;
    public DialogueUI DialogueUI;
    private ToolTipScript ToolTipStorage;

    // Start is called before the first frame update
    void Start()
    {
        DialogueUI = FindObjectOfType<DialogueUI>();
        stateManager = GameObject.FindWithTag("StateManager").GetComponent<StateManager>();
    }

    public void Interact()
    {
        if (!DialogueUI.CurrentlyActive)
        {
            //DialogueUI.StartDialogue(Dialogus.GetActiveDialogue(), gameObject.GetComponent<NPC>());
            DialogueUI.StartDialogue(Dialogus.GetActiveDialogue(), Dialogus);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject instance = GameObject.Instantiate(stateManager.InteractToolTip, (transform.position + (Vector3.up * ToolTipHeight)), this.transform.rotation);
            ToolTipStorage = instance.GetComponentInChildren<ToolTipScript>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToolTipStorage.Despawn();
        }
    }
}
