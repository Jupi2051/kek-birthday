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
        animator.SetTrigger("Despawn");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
