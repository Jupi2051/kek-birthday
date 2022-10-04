using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundReceiever : MonoBehaviour
{
    public MarioController controller;

    public void LeftStep()
    {
        controller.L.Play();
    }

    public void RightStep()
    {
       controller.R.Play();
    }
}
