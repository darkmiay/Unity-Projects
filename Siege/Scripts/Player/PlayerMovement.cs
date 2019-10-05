using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    const float locomationAnimationSmoothTime = 0f;
    public Animator animator;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    public bool onStairs = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float speedPercent;
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

             

               
            speedPercent = Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z);
            animator.SetFloat("SpeedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        
        if (onStairs)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0.0f );
            speedPercent = Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z);
            animator.SetFloat("SpeedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
        }

        // Apply gravity
        if(!onStairs)
        {
            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        }

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
