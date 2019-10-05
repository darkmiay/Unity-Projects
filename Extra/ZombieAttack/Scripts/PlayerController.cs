using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool inRotate = false;
    public bool blockControls = false;
    public float targetRotation;
    public float rotationSpeed;
  
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!blockControls)
        {        
        if (inRotate)
        {
            Quaternion startRotation = this.transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), Time.deltaTime * rotationSpeed);
      //      this.transform.rotation = Quaternion.Euler(0f, Mathf.Clamp(transform.rotation.y, startRotation, targetRotation), 0f);
            if (startRotation == transform.rotation) inRotate = false;
        }
        else
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                inRotate = true;
                targetRotation = transform.eulerAngles.y + 90f;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                inRotate = true;
                targetRotation = transform.eulerAngles.y - 90f;
            }
        }
        }
    }

    public void BlockControlls()
    {
        blockControls = true;
    }

    public void UnBlockControlls()
    {
        blockControls = false;
    }
}
