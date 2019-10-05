using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItoPlayerRotation : MonoBehaviour
{

    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
           this.transform.LookAt(playerTransform);
        }

    
}
