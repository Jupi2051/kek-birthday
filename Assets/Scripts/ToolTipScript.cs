using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipScript : MonoBehaviour
{
    public Animator animator;


    public void KillToolTip()
    {
        Destroy(transform.parent.gameObject);
    }
    
    public void Despawn()
    {
        if (animator != null) animator.SetTrigger("Despawn");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ToolTipScript[] list = FindObjectsOfType(typeof(ToolTipScript)) as ToolTipScript[];
        foreach (ToolTipScript script in list)
            if (!ReferenceEquals(script.gameObject, gameObject))
                script.Despawn();
    }
}
