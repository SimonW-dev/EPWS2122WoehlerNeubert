using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    //basic movement variables
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    [SerializeField] public float runSpeed = 40f;


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //Jump on Button press
        if (Input.GetButtonDown("Jump")) 
        {
            jump = true;
            animator.SetBool("Jump", true);
        }

        //Crouch as long as Button is held down
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("Crouch", true);
        } 
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("Crouch", false);
        }
    }

    //FixedUpdate has the frequency of the physics system; it is called every fixed frame-rate frame
    void FixedUpdate() 
    {
        //Move character
        //Time.fixedDeltaTime: The interval in seconds at which physics and other fixed frame rate updates
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); 

        //animation
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }
}

