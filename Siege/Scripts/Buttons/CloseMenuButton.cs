using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuButton : MonoBehaviour
{

    public GameObject menu;
    
    public void OnClick()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(false);
      
    }
}

