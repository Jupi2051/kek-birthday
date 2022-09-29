using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public StateManager stateManager;
    public int CurrentReplyIndex = 0;
    public bool ResetConversationOnEnd = true;
    public float ToolTipHeight = 0.5f;

    private ToolTipScript ToolTipStorage;


    public List<string> Replies = new()
    {
        "Alo Babi",
        "quit Talking",
        "you're getting so annoying",
        "dame da ne dame yo dame an no yo anta ga suki de suki sugite dore da ke",
    };

    public void Interact()
    {
        print(Replies[CurrentReplyIndex]);
        CurrentReplyIndex++;
        if (CurrentReplyIndex == Replies.Count)
        {
            CurrentReplyIndex = ResetConversationOnEnd? 0 : Replies.Count - 1;
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

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindWithTag("StateManager").GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
