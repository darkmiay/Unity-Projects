using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : Interactable
{
    public GameObject menu;

    public override void Interact()
    {
        Cursor.lockState = CursorLockMode.None;
        menu.SetActive(true);

    }
}
