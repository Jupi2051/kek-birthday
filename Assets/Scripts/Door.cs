using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public StateManager stateManager;
    public float ToolTipHeight = 0.5f;
    public ToolTipScript ToolTipStorage;

    public SyrenScreen syrenScreen;
    public Vector3 TeleportTo;
    public MarioController Player;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindWithTag("StateManager").GetComponent<StateManager>();
        syrenScreen = GameObject.FindWithTag("SyrenScreen").GetComponent<SyrenScreen>();
        Player = GameObject.FindWithTag("Player").GetComponent<MarioController>();
    }

    public void Interact()
    {
        Player.CanPlayerMove = false;
        syrenScreen.Fade();
        StartCoroutine("TeleportDelayed");
    }

    public IEnumerator TeleportDelayed()
    {
        yield return new WaitForSeconds(0.25f);
        Player.transform.position = new Vector3(TeleportTo.x, TeleportTo.y, Player.transform.position.z);

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
