using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteract : MonoBehaviour, IInteractable
{
    public StateManager stateManager;
    public float ToolTipHeight = 0.5f;
    public ToolTipScript ToolTipStorage;

    public MarioController Player;

    public bool isPlaying = false;
    public GameObject SolidGold;

    public GameObject InstanceOfSolidGold;

    public bool SoftLock = false;
    public bool SoftLock2 = false;

    public AudioSource Music;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindWithTag("StateManager").GetComponent<StateManager>();
        Player = GameObject.FindWithTag("Player").GetComponent<MarioController>();
        Music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (isPlaying == false && !SoftLock)
        {
            InstanceOfSolidGold = Instantiate(SolidGold);
            Player.CanPlayerMove = false;
            isPlaying = true;
            SoftLock2 = true;
            Music.Pause();
        }
        SoftLock = false;
    }

    public void Update()
    {
        if (isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E) && !SoftLock2)
            {
                isPlaying = false;
                Player.CanPlayerMove = true;
                SoftLock = true;
                Destroy(InstanceOfSolidGold);
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
