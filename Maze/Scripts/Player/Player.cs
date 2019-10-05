using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    private void Start()
    {
        if (isLocalPlayer)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public Inventory inventory;
    public PlayerInteract playerInteract;

    
}
