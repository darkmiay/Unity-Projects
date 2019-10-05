using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
   public float maxDistance;
   public Text interactionText;
   
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward,out hit, maxDistance))
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();
                if(interactable!=null)
                {
                interactionText.text = interactable.GetInteractionText();
                interactionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            } 
                
        }
        else
        {
            if (interactionText.gameObject.activeSelf)
            interactionText.gameObject.SetActive(false);
        }
    }
}
