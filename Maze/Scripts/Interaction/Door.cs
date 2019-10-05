using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Door : Interactable {

    public bool needKey;

    public override void Interact(Player player)
    {
        if (needKey)
        {
            Item key = player.inventory.FindItem(ItemType.Key);
            if (key != null)
            {
                player.inventory.RemoveItem(key);
                this.GetComponent<Animator>().SetBool("open", true);
            }
            else
            Debug.Log("No key found");
        }
        else
        {
            this.GetComponent<Animator>().SetBool("open", true);
        }

        
    }


    [ClientRpc]
    void RpcRemoveItem(GameObject player)
    {
        player.GetComponent<Player>().inventory.RemoveItem(player.GetComponent<Player>().inventory.FindItem(ItemType.Key));
    }
}
