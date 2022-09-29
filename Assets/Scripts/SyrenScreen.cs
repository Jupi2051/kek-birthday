using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrenScreen : MonoBehaviour
{
    public Animator animator;
    public MarioController Player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Player = GameObject.FindWithTag("Player").GetComponent<MarioController>();
    }

    public void Fade()
    {
        animator.SetTrigger("ScreenFade");
    }

    public void EnableMovement()
    {
        Player.EnableMovement();
    }
}
