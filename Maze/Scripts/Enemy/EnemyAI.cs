using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class EnemyAI : NetworkBehaviour {

   public NavMeshAgent agent;
   public GameObject target;

	void Start () {

        if (!isServer)
        {
            return;
        }
              agent = GetComponent<NavMeshAgent>();

        this.transform.position = GetComponentInParent<Transform>().position;
    }
	
	void Update () {
           if (!isServer)
           {
                return;
           }
        target = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(target.transform.position);


    }
}
