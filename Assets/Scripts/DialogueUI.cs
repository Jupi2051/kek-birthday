using Assets.Scripts.OOPWork;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public StateManager stateManager;
    public GiftManager GiftManager;

    public TMP_Text SpeechText;
    public TMP_Text SpeakerName;
    public Image SpeakerPicture;

    public Canvas canvas;

    public bool CurrentlyActive = false;
    public SpeechUnit CurrentDialogue;
    public SpeechUnits CurrentNPC;

    public string FullSpeech = "";
    public int TextSplice = 0;
    public bool ProgressiveDrawing = false;

    private readonly int MaxTick = 20;
    private int TickStep = 1;

    public AudioSource DialogueAudio;

    public string LastKnownCallfunction;
    // Start is called before the first frame update
    void Start()
    {
        GameObject IdentityBox = GameObject.Find("SpeakingContainer");
        GameObject TextBox = GameObject.Find("SpeechContainer");

        canvas = GetComponent<Canvas>();

        SpeechText = TextBox.GetComponentInChildren<TMP_Text>();

        SpeakerName = IdentityBox.GetComponentInChildren<TMP_Text>();
        SpeakerPicture = IdentityBox.GetComponentInChildren<Image>();

        stateManager = FindObjectOfType<StateManager>();
        DialogueAudio = GetComponentInChildren<AudioSource>();

        GiftManager = FindObjectOfType<GiftManager>();
    }

    public void StartDialogue(SpeechUnit Dialogue, SpeechUnits Dialogues)
    {
        canvas.enabled = true;
        CurrentlyActive = true;
        CurrentDialogue = Dialogue;
        CurrentNPC = Dialogues;
        Tuple<bool, SpeechSingleTextContent> Reply = Dialogue.Next();
        SpeakText(Reply.Item2.Speech, Reply.Item2.CharacterName, stateManager.GetSpriteByName(Reply.Item2.CharacterName));
        if (Reply.Item1) Dialogues.MoveToNextDialogue();
    }

    public void RollText()
    {
        if (!ProgressiveDrawing)
        {
            Tuple<bool, SpeechSingleTextContent> Reply = CurrentDialogue.Next();
            SpeakText(Reply.Item2.Speech, Reply.Item2.CharacterName, stateManager.GetSpriteByName(Reply.Item2.CharacterName));
            if (Reply.Item2.TriggerFunction != null) LastKnownCallfunction = Reply.Item2.TriggerFunction;
            if (Reply.Item1)
            {
                CurrentlyActive = false;
                CurrentNPC.MoveToNextDialogue();
                canvas.enabled = false;
                if (LastKnownCallfunction != null && LastKnownCallfunction != "")
                {
                    GiftManager.BroadcastMessage(LastKnownCallfunction);
                }
                LastKnownCallfunction = null;
            }
        }
        else
            SkipDrawing();
    }

    public void SpeakText(string Text, string CharacterName, Sprite CharacterImage = null)
    {

        FullSpeech = Text;
        SpeakerName.text = CharacterName;
        SpeakerPicture.sprite = CharacterImage;
        ProgressiveDrawing = true;
        StartCoroutine(nameof(DrawText));
    }

    public void SkipDrawing()
    {
        if (ProgressiveDrawing)
        {
            StopCoroutine(nameof(DrawText));
            ProgressiveDrawing = false;
            SpeechText.text = FullSpeech;
        }
    }

    public IEnumerator DrawText()
    {
        if (ProgressiveDrawing)
        {
            for (int LengthOfText = 0; LengthOfText <= FullSpeech.Length; LengthOfText++)
            {
                SpeechText.text = FullSpeech[..LengthOfText];
                DialogueAudio.Play();
                yield return new WaitForSeconds(0.05f);
            }
            ProgressiveDrawing = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ProgressiveDrawing)
        {
            TickStep++;
            if (TickStep > MaxTick)
            {
                TickStep = 0;
            }
        }
    }
}
