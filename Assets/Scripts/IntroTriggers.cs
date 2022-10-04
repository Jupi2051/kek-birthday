using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class IntroTriggers : MonoBehaviour
{
    public Animator DarkBG;
    public Animator BG;
    public Animator Birds;
    public Animator Text;

    public TMP_Text ZZZText;

    public bool SoftLockForDarkBG;
    public bool SoftLockForWriting;

    public AudioSource Music;

    public MarioController Kek;

    public bool ReadyToMove = false;

    // Start is called before the first frame update
    void Start()
    {
        ZZZText = Text.gameObject.GetComponent<TMP_Text>();
        Music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        Kek = FindObjectOfType<MarioController>();
        ReadyToMove = false;
        //StartCoroutine(nameof(WaitForFewSeconds));
    }

    IEnumerator FadeInBackground()
    {
        yield return new WaitForSeconds(1);
        DarkBG.SetTrigger("Start");
        Birds.SetTrigger("Start");
    }

    IEnumerator ZTextWriter()
    {
        string FullText = "Zzz...";
        for (int x = 0; x <= FullText.Length; x++)
        {
            ZZZText.text = FullText[..x];
            yield return new WaitForSeconds(0.4f);
        }
        SoftLockForDarkBG = true;
        StartCoroutine(nameof(FadeInBackground));
        Text.SetTrigger("Start");
        int ExternalCounter = 0;
        while (true)
        {
            if (ExternalCounter == FullText.Length) ExternalCounter = 0;
            ZZZText.text = FullText[..ExternalCounter];
            ExternalCounter++;
            yield return new WaitForSeconds(0.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Kek.CanPlayerMove = ReadyToMove;

        if (!SoftLockForWriting)
        {
            SoftLockForWriting = true;
            StartCoroutine(nameof(ZTextWriter));
        }
    }

    public void BirdsEnding()
    {
        DarkBG.SetTrigger("End");
        Invoke(nameof(DropDialogue), 2);

    }

    public void DropDialogue()
    {
        FindObjectOfType<LevelManager>().RoomDialogue();
        Invoke(nameof(TurnOnGame), 1);
        Music.Play();
    }

    public void TurnOnGame()
    {
        Birds.gameObject.SetActive(false);
        Text.gameObject.SetActive(false);
        BG.gameObject.SetActive(false);
        DarkBG.SetTrigger("Start");
        ReadyToMove = true;
    }
}
