using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderInteract : MonoBehaviour, IInteractable
{
    public StateManager stateManager;
    private ToolTipScript ToolTipStorage;
    public float ToolTipHeight = 0.5f;

    void Start()
    {
        stateManager = GameObject.FindWithTag("StateManager").GetComponent<StateManager>();
    }

    public void Interact()
    {
        SceneManager.LoadScene(1);
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
