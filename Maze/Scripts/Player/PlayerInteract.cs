using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInteract : NetworkBehaviour {

    Interactable lastInteractable;


    public void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        Camera camera = GetComponentInChildren<Camera>();
        if (camera == null)
        {
            return;
        }
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Interactable interactable = hit.transform.GetComponentInParent<Interactable>();
            if (interactable != null)
            {

                if (interactable != lastInteractable)
                {
                    if (lastInteractable!=null)
                    lastInteractable.HideText();
                    lastInteractable = interactable;
                    lastInteractable.ShowText();
                }
                else
                {
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    CmdInteract(lastInteractable.gameObject);
                }
            }
            else
            {
                if (lastInteractable != null)
                    lastInteractable.HideText();
                lastInteractable = null;
            }

        }
        else
        {
            if (lastInteractable != null)
                lastInteractable.HideText();
            lastInteractable = null;
        }
    }

    [Command]
    void CmdInteract(GameObject lastInteractable)
    {
        lastInteractable.GetComponent<Interactable>().Interact(GetComponent<Player>());
    }
}
