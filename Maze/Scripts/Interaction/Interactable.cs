using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Interactable : NetworkBehaviour {

    public string name;
    public GameObject textCanvas;

    public void Start()
    {
        ShowText();
        textCanvas.GetComponentInChildren<Text>().text = "Interact with " + name;
        HideText();
    }

    public virtual void Interact(Player player)
    {
        Debug.Log(player+" interact with "+name);
    }

    public void ShowText()
    {
        textCanvas.SetActive(true);
    }

    public void HideText()
    {
        textCanvas.SetActive(false);
    }
}
