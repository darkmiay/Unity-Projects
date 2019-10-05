using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Pickupable : Interactable {

    public Item item;

    
    public override void Interact(Player player)
    {
        player.inventory.AddItem(item);

        RpcAddItem(player.gameObject);
        NetworkServer.Destroy(this.gameObject);
    }

    [ClientRpc]
    void RpcAddItem(GameObject player)
    {
        player.GetComponent<Player>().inventory.AddItem(this.item);
    }
}
