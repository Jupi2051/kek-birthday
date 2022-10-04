using Assets.Scripts.OOPWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    public StateManager stateManager;
    public float ToolTipHeight = 0.5f;
    public ToolTipScript ToolTipStorage;

    public MarioController Player;

    public bool isPlaying = false;

    public bool SoftLock = false;
    public bool SoftLock2 = false;

    public AudioSource Music;
    public AudioSource RayMusic;



    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindWithTag("StateManager").GetComponent<StateManager>();
        Player = GameObject.FindWithTag("Player").GetComponent<MarioController>();
        Music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        RayMusic = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (isPlaying == false && !SoftLock && !Player.DialogueUI.CurrentlyActive)
        {
            //InstanceOfSolidGold = Instantiate(SolidGold);
            RayMusic.Play();
            Music.Pause();
            isPlaying = true;
            SoftLock2 = true;

            if (gameObject.CompareTag("RoomTag")) return;
            SpeechUnits units = new SpeechUnits(new List<SpeechUnit>()
            {
                new SpeechUnit(new List<SpeechSingleTextContent>()
                {
                    new SpeechSingleTextContent("Raydio", "Hello ladies and gentlemen and welcome to another episode of your favourite show RAYDIO!"),
                    new SpeechSingleTextContent("Raydio", "And for a track from one of our best producers out there!"),
                    new SpeechSingleTextContent("Raydio", "RAY!!"),
                    new SpeechSingleTextContent("Kek", "*listens*"),
                })
            });

            Player.DialogueUI.StartDialogue(units.GetActiveDialogue(), units);
        }
        SoftLock = false;
    }

    public void Update()
    {
        if (isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E) && !SoftLock2 && !Player.DialogueUI.CurrentlyActive)
            {
                RayMusic.Stop();
                isPlaying = false;
                SoftLock = true;
                Music.Play();
            }
            SoftLock2 = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject instance = Instantiate(stateManager.InteractToolTip, (transform.position + (Vector3.up * ToolTipHeight)), this.transform.rotation);
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
