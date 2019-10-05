using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawn : NetworkBehaviour {

	public void SpawnPlayer()
    {
        RpcSpawnPlayer();
    }

    [ClientRpc]
    public void RpcSpawnPlayer()
    {
       this.transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
    }

}
