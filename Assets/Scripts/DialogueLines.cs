using Assets.Scripts.OOPWork;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLines : MonoBehaviour
{
    public SpeechSingleContentStruct[] Lines1;
    public SpeechSingleContentStruct[] Lines2;
    public SpeechSingleContentStruct[] Lines3;
    public SpeechSingleContentStruct[] Lines4;
    public SpeechSingleContentStruct[] Lines5;
    public SpeechSingleContentStruct[] Lines6;
    public SpeechSingleContentStruct[] Lines7;

    // Start is called before the first frame update
    void Start()
    {
        NPC npc = GetComponent<NPC>();

        List<SpeechUnit> Talks = new List<SpeechUnit>();

        List<SpeechSingleTextContent> C1 = new();
        foreach (SpeechSingleContentStruct singleLine in Lines1)
            C1.Add(new SpeechSingleTextContent(singleLine.CharacterName, singleLine.Speech, singleLine.TriggerFunction));
        if (Lines1.Length != 0)
            Talks.Add(new SpeechUnit(C1));

        List<SpeechSingleTextContent> C2 = new();

        foreach (SpeechSingleContentStruct singleLine in Lines2)
            C2.Add(new SpeechSingleTextContent(singleLine.CharacterName, singleLine.Speech, singleLine.TriggerFunction));
        
        if (Lines2.Length != 0)
            Talks.Add(new SpeechUnit(C2));

        List<SpeechSingleTextContent> C3 = new();

        foreach (SpeechSingleContentStruct singleLine in Lines3)
            C3.Add(new SpeechSingleTextContent(singleLine.CharacterName, singleLine.Speech, singleLine.TriggerFunction));
        if (Lines3.Length != 0)
            Talks.Add(new SpeechUnit(C3));

        List<SpeechSingleTextContent> C4 = new();
        foreach (SpeechSingleContentStruct singleLine in Lines4)
            C4.Add(new SpeechSingleTextContent(singleLine.CharacterName, singleLine.Speech, singleLine.TriggerFunction));
        if (Lines4.Length != 0)
            Talks.Add(new SpeechUnit(C4));

        List<SpeechSingleTextContent> C5 = new();
        foreach (SpeechSingleContentStruct singleLine in Lines5)
            C5.Add(new SpeechSingleTextContent(singleLine.CharacterName, singleLine.Speech, singleLine.TriggerFunction));
        if (Lines5.Length != 0)
            Talks.Add(new SpeechUnit(C5));

        List<SpeechSingleTextContent> C6 = new();
        foreach (SpeechSingleContentStruct singleLine in Lines6)
            C6.Add(new SpeechSingleTextContent(singleLine.CharacterName, singleLine.Speech, singleLine.TriggerFunction));
        if (Lines6.Length != 0)
            Talks.Add(new SpeechUnit(C6));

        List<SpeechSingleTextContent> C7 = new();
        foreach (SpeechSingleContentStruct singleLine in Lines7)
            C7.Add(new SpeechSingleTextContent(singleLine.CharacterName, singleLine.Speech, singleLine.TriggerFunction));
        if (Lines7.Length != 0)
            Talks.Add(new SpeechUnit(C7));

        npc.Dialogus = new SpeechUnits(Talks, npc.ResetConversationOnEnd);
    }
}
