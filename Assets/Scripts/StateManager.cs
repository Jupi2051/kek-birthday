using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject InteractToolTip;
    public string[] CharacterNames;
    public Sprite[] CharacterImages;

    public Sprite GetSpriteByName(string Name)
    {
        for (int i = 0; i < CharacterNames.Length; i++)
            if (CharacterNames[i] == Name)
                return CharacterImages[i];
        return null;
    }
}
