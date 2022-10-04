using Assets.Scripts.OOPWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public SpeechUnits Dialogus;
    public DialogueUI DialogueUI;

    public SpeechUnits units2 = new SpeechUnits(new List<SpeechUnit>()
        {
            new SpeechUnit(new List<SpeechSingleTextContent>()
            {
                new SpeechSingleTextContent("???", "JUMP OUT OF YOUR WINDOW!!"),
            })
        });

    public void Start()
    {
        DialogueUI = FindObjectOfType<DialogueUI>();
        if (gameObject.CompareTag("RoomTag")) return;
        SpeechUnits units = new SpeechUnits(new List<SpeechUnit>()
        {
            new SpeechUnit(new List<SpeechSingleTextContent>()
            {
                new SpeechSingleTextContent("Everyone", "SURPRISE!!!!!!!!!"),
                new SpeechSingleTextContent("Everyone", "HAPPY BIRTHDAY!!!")
            })
        });

        DialogueUI.StartDialogue(units.GetActiveDialogue(), units);
    }

    public void RoomDialogue()
    {
        DialogueUI.StartDialogue(units2.GetActiveDialogue(), units2);

    }

}
