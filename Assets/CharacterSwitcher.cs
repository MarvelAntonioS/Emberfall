using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characters;  // Array of your characters
    private int activeCharacterIndex = 0;

    void Start()
    {
        ActivateCharacter(activeCharacterIndex);  // Activate the first character at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))  // Press Tab to switch characters
        {
            activeCharacterIndex = (activeCharacterIndex + 1) % characters.Length;
            ActivateCharacter(activeCharacterIndex);
        }
    }

    void ActivateCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            // Enable the active character and disable all others
            characters[i].SetActive(i == index);
        }
    }
}
