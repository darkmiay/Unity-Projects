using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Vector3 target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Player") GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().KillPlayer();
    }
}

